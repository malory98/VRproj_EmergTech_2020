using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject enemy;

    public Enemy enemyScript;
    public PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerScript.health--;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "Weapon" && enemyScript.state == Enemy.State.Attack)
        {
            enemyScript.TakePostureDMG();
        }
    }
}




