using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/*******************************
 Name: Lyssa Tino
 Course: Emerging Technologies
 Project: Wake Up
*******************************/

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public GameObject player;

    //UI references
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public GameObject timer;
    public TextMeshProUGUI timerText;
    public GameObject loseLifePanel;
    public GameObject gameOverPanel;

    public void LoseLife()
    {
        lives--;
        loseLifePanel.SetActive(true);
        StartCoroutine(TurnOffPanel(loseLifePanel));

        //turns UI hearts on and off
        switch (lives)
        {
            case 2:
                life3.SetActive(false);
                break;
            case 1:
                life2.SetActive(false);
                break;
            case 0:
                life1.SetActive(false);
                ZeroLives();
                break;
        }
    }

    public void ZeroLives()
    {
        //restart from beginning
        lives = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverPanel.SetActive(true);
        StartCoroutine(TurnOffPanel(gameOverPanel));

        //turn on UI hearts
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void UnpauseTime()
    {
        Time.timeScale = 1;
    }

    public IEnumerator TurnOffPanel(GameObject myObject)
    {
        myObject.SetActive(false);
        yield return new WaitForSeconds(4);
    }
}
