using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int health;
    public int maxHP;
    public GameManager GM;

    public IEnumerator LifeLost()
    {
        GM.LoseLife();
        health = maxHP;
        yield return new WaitForSeconds(4);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
        {
            StartCoroutine("LifeLost");
        }
    }
}
