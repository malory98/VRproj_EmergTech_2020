using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public State state;

    public GameManager GM;

    public Vector3 direction;

    public Rigidbody rb;

    public GameObject enemy;
    public GameObject player;

    public int postureHP;
    public int maxPostureHP;
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
            yield return new WaitForSeconds(recoverySpeed);
            StartCoroutine("ExposedTimer");
        }
    }

    // Method to reduce enemy's posture HP and trigger parry animation
    public void TakePostureDMG()
    {
        animator.SetTrigger("Parried");
        postureHP--;
        Debug.Log(postureHP);
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            if (state == State.Exposed)
            {
                health--;
            }

            else
            {
                health = health - (1 * 0.5f);
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Begin;
        isAttacking = false;
    }

    void FixedUpdate () 
    {
 
        switch (state)
        {
            #region Begin State
            // Begin State -> Checks if the player is within the start combat zone
            case State.Begin:

                currentDist = Vector3.Magnitude(enemy.transform.position - player.transform.position);
                Debug.Log(currentDist);
               
                if (currentDist < agroRange)
                {
                    state = State.Chase;
                }

                break;
            #endregion

            #region Chase State
            // Chase State -> Moves enemy in combat range of player
            case State.Chase:

                // Obtains the direction vector of the enemy to player and the current distance between them
                currentDist = Vector3.Magnitude(enemy.transform.position - player.transform.position);
                Debug.Log(currentDist);
                direction = (player.transform.position - enemy.transform.position);

                // Moves the position of the enemy towards the player
                rb.MovePosition(transform.position + (direction * enemySpeed * Time.fixedDeltaTime));

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
                }

                break;
            #endregion

            #region Attack State
            // Attack State -> Fires attack triggers every set amount of seconds
            case State.Attack:
                // Triggers the attack coroutine
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

                // Sets State to dead when enemy hp hits zero
                if (health < 1)
                {
                    state = State.Dead;
                }
                
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
                }

                break;
            #endregion

            case State.Dead:
                
                break;

        }

    }

}
