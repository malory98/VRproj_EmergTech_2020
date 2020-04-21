using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject enemy;

    public Enemy enemyScript;
    public PlayerScript playerScript;

    public GameObject playerWeapon;
    public VelocityEstimator velocityEstimator;
    public float playerWeaponSpeed;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    void OnTriggerEnter(Collider other)
    {
        velocityEstimator = playerWeapon.GetComponent<VelocityEstimator>();
        playerWeaponSpeed = velocityEstimator.GetVelocityEstimate().magnitude;

        if (other.gameObject.tag == "Player")
        {
            playerScript.health--;
            playerScript.healthSlider.value = playerScript.health;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "Weapon" && enemyScript.state == Enemy.State.Attack && playerWeaponSpeed > 2)
        {
            enemyScript.TakePostureDMG();
        }
    }
}




