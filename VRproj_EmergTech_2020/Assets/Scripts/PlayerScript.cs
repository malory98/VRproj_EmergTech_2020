using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    static PlayerScript instance;

    public int health;
    public int maxHP;
    public GameManager GM;
    public Slider healthSlider;

    public IEnumerator LifeLost()
    {
        GM.LoseLife();
        health = maxHP;
        yield return new WaitForSeconds(4);
    }

    // Start is called before the first frame update
    void Start()
    {
        //sets to singleton
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        healthSlider.maxValue = maxHP;
        healthSlider.value = health;
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
