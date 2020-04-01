using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public Transform spawnpoint;
    public GameObject player;


    public void LoseLife()
    {
        lives--;
        CheckLives();
    }

    public void CheckLives()
    {
        if (lives == 0)
        {
            ZeroLives();
        }
    }

    public void ZeroLives()
    {
        //restart from beginning
        player.transform.position = spawnpoint.position;
        lives = 3;
    }
}
