using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public GameObject enemyScriptObj;
    public EnemyScript enemyScript;
    public bool inCollision;

    // Start is called before the first frame update
    void Start()
    {
        enemyScriptObj = GameObject.FindGameObjectWithTag("Enemy");
        enemyScript = enemyScriptObj.GetComponent<EnemyScript>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy_Sword" && enemyScript.isImmortal && !inCollision && enemyScript.canParry)
        {
            Debug.Log("Sword has collided");
            enemyScript.DecreaseParryCounter();
            inCollision = true;
        }
        else
        {
            Debug.Log("Attack was blocked");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Enemy_Sword" && enemyScript.isImmortal && inCollision)
        {
            inCollision = false;
            Debug.Log(inCollision.ToString());
        }
    }
}
