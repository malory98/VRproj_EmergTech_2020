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
    static GameManager instance;

    public int lives = 3;
    public GameObject player;
    public int totalLevels;
    public int levelsPassed = 0;

    //UI references
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public GameObject timer;
    public TextMeshProUGUI timerText;
    public GameObject loseLifePanel;
    public GameObject gameOverPanel;
    public GameObject winGamePanel;
    public GameObject pausePanel;
    public GameObject canvasMenus;

    public void Start()
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
    }

    public void LoseLife()
    {
        lives--;
        canvasMenus.SetActive(true);
        pausePanel.SetActive(false);
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
                //ZeroLives();
                GameOver();
                break;
        }
    }

    public void GameOver()
    {
        canvasMenus.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void ZeroLives()  // Restart Level
    {
        //restart from beginning
        //lives = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public void WinGame()
    {
        canvasMenus.SetActive(true);
        pausePanel.SetActive(false);
        winGamePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PassLevel()
    {
        levelsPassed++;
        if (levelsPassed == totalLevels)
        {
            WinGame();
        }
    }
}
