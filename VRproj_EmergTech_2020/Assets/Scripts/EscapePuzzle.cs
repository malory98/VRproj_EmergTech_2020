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
    public float timeLeft = 60.0f;
    public int timeToDisplay = 0;
    public GameManager gm;

    //UI references
    public Text timeText;

    void Update()
    {
        //start counting down if the puzzle has started
        if (started)
        {
            timeLeft -= Time.deltaTime;

            //typecast and round for timer display
            timeToDisplay = (int)Mathf.Round(timeLeft);

            //display new time on HUD
            timeText.text = timeToDisplay.ToString();

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
            started = true;
        }
    }
}
