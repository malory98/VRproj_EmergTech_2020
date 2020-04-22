using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Begin = 0,
        Chase = 1,
        Attack = 2,
        Exposed = 3,
        Dead = 4
    };

    #region Variables
    NavMeshAgent _navMeshAgent; 
    
    public State state;

    public GameManager GM;

    public Vector3 direction;

    public Rigidbody rb;

    public GameObject enemy;
    public GameObject playerWeapon;
    public VelocityEstimator velocityEstimator;
    public GameObject player;

    public int postureHP;
    public int maxPostureHP;

    public float playerWeaponSpeed;
    public float health;
    public float maxHP;

    public float currentDist;
    public float agroRange;
    public float attackRange;

    public float attackSpeed;
    public float recoverySpeed;
    public float enemySpeed;

    public Animator animator;

    public bool isAttacking;
    public bool isExposed;

    public Slider healthSlider;
    public Slider postureSlider;
    #endregion

    // Fires an attack animation trigger then waits
    public IEnumerator StartAttack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }

    // Refills Posture HP and set state to Attack
    public IEnumerator ExposedTimer()
    {
        if (postureHP != maxPostureHP && state == State.Exposed)
        {
            postureHP++;
            postureSlider.value = postureHP;
            yield return new WaitForSeconds(recoverySpeed);
            StartCoroutine("ExposedTimer");
        }
    }

    // Method to reduce enemy's posture HP and trigger parry animation
    public void TakePostureDMG()
    {
        animator.SetTrigger("Parried");
        postureHP--;
        postureSlider.value = postureHP;
        Debug.Log(postureHP);
    }

    void OnCollisionEnter (Collision collision)
    {
        velocityEstimator = playerWeapon.GetComponent<VelocityEstimator>();
        playerWeaponSpeed = velocityEstimator.GetVelocityEstimate().magnitude;
        Debug.Log(playerWeaponSpeed);

        if (collision.gameObject.tag == "Weapon" && playerWeaponSpeed > 2)
        {

            if (state == State.Exposed)
            {
                health--;
                healthSlider.value = health;
            }

            else
            {
                health = health - (1 * 0.5f);
                healthSlider.value = health;
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Begin;
        isAttacking = false;
        healthSlider.maxValue = health;
        healthSlider.value = health;
        player = GameObject.Find("Player");
        postureSlider.maxValue = postureHP;
        postureSlider.value = postureHP;
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    void FixedUpdate () 
    {
        switch (state)
        {
            #region Begin State
            // Begin State -> Checks if the player is within the start combat zone
            case State.Begin:

                // Obtains the direction vector of the enemy to player and the current distance between them
                currentDist = Vector3.Magnitude(enemy.transform.position - player.transform.position);
                //Debug.Log(currentDist);

                if (currentDist < agroRange)
                {
                    state = State.Chase;
                }
                
                // Sets State to dead when enemy hp hits zero
                if (health < 1)
                {
                    state = State.Dead;
                    Debug.Log("Enemy died in " + state + "State");
                }
                break;
            #endregion

            #region Chase State
            // Chase State -> Moves enemy in combat range of player
            case State.Chase:

                // Obtains the direction vector of the enemy to player and the current distance between them
                currentDist = Vector3.Magnitude(enemy.transform.position - player.transform.position);
                
                //Debug.Log(currentDist);
                direction = (player.transform.position - enemy.transform.position);

                // Moves the position of the enemy towards the player
                //rb.MovePosition(transform.position + (direction * enemySpeed * Time.fixedDeltaTime));

                _navMeshAgent.SetDestination(player.transform.position);

                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

                animator.SetBool("IsMoving", true);
                
                // Checks if the cuirrent distance is within attack range
                if (currentDist < attackRange)
                {
                    animator.SetBool("IsMoving", false);
                    state = State.Attack;
                }


                // Sets State to dead when enemy hp hits zero
                if (health < 1)
                {
                    state = State.Dead;
                    Debug.Log("Enemy died in " + state + "State");
                }

                break;
            #endregion

            #region Attack State
            // Attack State -> Fires attack triggers every set amount of seconds
            case State.Attack:
                
                // Obtains the direction vector of the enemy to player and the current distance between them
                currentDist = Vector3.Magnitude(enemy.transform.position - player.transform.position);
                //Debug.Log(currentDist);

                if (currentDist < attackRange)
                {
                    if (!isAttacking)
                    {
                        StartCoroutine("StartAttack");
                        isAttacking = true;
                    }
                
                    // Sets the State to exposed when the player breaks the posture meter
                    if (postureHP < 1)
                    {
                        state = State.Exposed;
                        isAttacking = true;
                        animator.SetBool("Exposed", true);
                    }

                }
                else
                {
                    state = State.Chase;
                }

                // Sets State to dead when enemy hp hits zero
                if (health < 1)
                {
                    state = State.Dead;
                    Debug.Log("Enemy died in " + state + "State");
                }
                // Triggers the attack coroutine
                
                break;

            #endregion

            #region Exposed State
            // Exposed State -> Leaves the enemy open to attacks for a set period of time
            case State.Exposed:
                // Ensures that the enemy's posture HP starts regenerating from 0
                if (postureHP < 0)
                {
                    postureHP = 0;
                }
               
                if (!isExposed)
                {
                    StartCoroutine("ExposedTimer");
                    isExposed = true;
                }

                if (postureHP == maxPostureHP)
                {
                    state = State.Attack;
                    isExposed = false;
                    animator.SetBool("Exposed", false);
                }

                // Sets State to dead when enemy hp hits zero
                if (health < 1)
                {
                    state = State.Dead;
                    Debug.Log("Enemy died in " + state + "State");
                }

                break;
            #endregion

            #region Dead State
            case State.Dead:
                animator.SetTrigger("Dies");
                GM.PassLevel();
                break;
            #endregion
        }

    }

}
