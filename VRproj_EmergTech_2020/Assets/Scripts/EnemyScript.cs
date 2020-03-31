using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public Animator enemyAnimator;

    public int parryCount;
    public int hitWindow;
    public int health;
    public int exposedTimer;


    public bool isImmortal;
    public bool inCollision;
    public bool canParry;

    public Text healthCounter;
    public Text parryCounter;
    public Text enemyState;
  
    // Start is called before the first frame update
    void Start()
    {
        canParry = false;
        isImmortal = true;
        parryCount = 7;
        hitWindow = 7;
        health = 100;
        parryCounter.text = "PARRY COUNT: " + parryCount.ToString();
        healthCounter.text = "HEALTH: " + health.ToString();
        enemyState.text = "STATE: DEFENDING";
        enemyAnimator = this.gameObject.GetComponent<Animator>();
        enemyAnimator.SetInteger("AtkSetCount", parryCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Enumerator for exposed state of enemy
    public IEnumerator BecomeExposed()
    {
        // Makes enemy mortal for set timer (exposed state)
        isImmortal = false;
        hitWindow = 7;
        Debug.Log("THE ENEMY IS EXPOSED");
        enemyState.text = "STATE: EXPOSED";
        parryCounter.text = "PARRY COUNT: " + parryCount.ToString();

        yield return new WaitForSeconds(exposedTimer);

        // Sets the enemy back into its attacking state
        isImmortal = true;
        parryCount = 7;
        Debug.Log("THE ENEMY ENTERED ITS ATTACK STATE");
        enemyState.text = "STATE: ATTACKING";
        parryCounter.text = "PARRY COUNT: " + parryCount.ToString();
        enemyAnimator.SetTrigger("EnemyRecovered");
        enemyAnimator.SetInteger("AtkSetCount", parryCount);

    }
    
    public void ToggleParryInvulnerabiltiy()
    {
        if (canParry)
        {
            canParry = false;
        }
        else
        {
            canParry = true;
        }
    }

    // Decreases the parry counter to be called on collision
    public void DecreaseParryCounter()
    {
        ToggleParryInvulnerabiltiy();
        parryCount --;
        ChangeState();
        parryCounter.text = "PARRY COUNT: " + parryCount.ToString();
        Debug.Log("The enemy can block " + parryCount + " more times.");
        enemyAnimator.SetTrigger("AtkParried");
        enemyAnimator.SetInteger("AtkSetCount", parryCount);
    }

    // Method to change enemy state (calls enumerator and changes bools)
    public void ChangeState()
    {
        // Checks if enemy is entering the exposed state
        if (isImmortal && parryCount <= 0)
        {
            StartCoroutine("BecomeExposed");
        }
   
        // Checks if the enemy is dead
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Checks enemy state to see if we can damage the enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon" && !isImmortal && !inCollision)
        {
            hitWindow--;
            health--;
            ChangeState();
            Debug.Log("The enemy has " + health + " health left.");
            healthCounter.text = "HEALTH: " + health.ToString();
            inCollision = true;
        }
        else if (collision.gameObject.tag == "Weapon" && isImmortal)
        {
            Debug.Log("THE ENEMY IS IN DEFENSE STATE");
        }
    }

    // Checks if the player sword is simply held inside enemy
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon" && !isImmortal && inCollision)
        {
            inCollision = false;
        }
    }
}
