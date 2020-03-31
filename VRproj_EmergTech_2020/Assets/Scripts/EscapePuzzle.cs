using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePuzzle : MonoBehaviour
{
    public bool started = false;
    public float timeLeft = 60.0f;
    public int timeToDisplay = 0;
    public int lives = 3;

    void Update()
    {
        //start counting down if the puzzle has started
        if (started)
        {
            timeLeft -= Time.deltaTime;

            //typecast and round for timer display
            timeToDisplay = (int)Mathf.Round(timeLeft);
            Debug.Log(timeToDisplay);

            //lose a life if time runs out
            if (timeLeft < 0)
            {
                lives--;
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
