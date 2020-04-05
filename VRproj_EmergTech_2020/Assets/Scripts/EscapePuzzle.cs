using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************************
 Name: Lyssa Tino
 Course: Emerging Technologies
 Project: Wake Up
*******************************/

public class EscapePuzzle : MonoBehaviour
{
    public bool started = false;
    public float totalTime;
    public float timeLeft;
    public int timeToDisplay = 0;
    public GameManager gm;

    void Update()
    {
        //start counting down if the puzzle has started
        if (started)
        {
            timeLeft = totalTime;
            timeLeft -= Time.deltaTime;

            //typecast and round for timer display
            timeToDisplay = (int)Mathf.Round(timeLeft);

            //display new time on HUD
            gm.timerText.text = timeToDisplay.ToString();

            //lose a life if time runs out
            if (timeLeft < 0)
            {
                gm.LoseLife();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //start the puzzle when player enters the zone
        if (other.gameObject.CompareTag("Player"))
        {
            gm.timer.SetActive(true);
            started = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //stop the puzzle when player exits the zone
        if (other.gameObject.CompareTag("Player"))
        {
            started = false;
            gm.timer.SetActive(false);
            timeLeft = totalTime;
        }
    }
}
