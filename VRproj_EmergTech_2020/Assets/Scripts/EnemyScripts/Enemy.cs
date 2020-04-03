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
        Exposed = 3
    };

    public State state;

    public Vector3 playerPos;
    public Vector3 thisPos;

    public int postureHP;
    public int maxPostureHP;
    public int health;
    public float attackSpeed;
    public float recoverySpeed;


    public Animator animator;

    public bool isAttacking;
    public bool isExposed;


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

    public void TakeDMG()
    {
        animator.SetTrigger("Parried");
        postureHP--;
        Debug.Log(postureHP);
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Begin;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            // Begin State -> Checks if the player is within the start combat zone
            case State.Begin:
                state = State.Chase;
                break;

            // Chase State -> Moves enemy in combat range of player
            case State.Chase:
                state = State.Attack;
                break;

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
                break;

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
                    animator.SetBool("Exposed", false) ;
                }
                break;

        }
    }
}
