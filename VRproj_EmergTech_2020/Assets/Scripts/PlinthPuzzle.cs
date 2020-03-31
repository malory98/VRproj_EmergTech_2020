using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************
 Name: Lyssa Tino
 Course: Emerging Technologies
 Project: Nightmare
*******************************/

public class PlinthPuzzle : MonoBehaviour
{

    public string correctItemName;
    public GameObject door;
    public int lives = 3;

    //get the info of the item on the plinth
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(CheckAnswer(collision));
    }

    //checks if answer is the correct item
    IEnumerator CheckAnswer(Collision collision)
    {
        if (collision.gameObject.CompareTag(correctItemName))
        {
            door.SetActive(false);
            Debug.Log("CORRECT ANSWER");
        }
        else
        {
            lives--;
            Debug.Log("WRONG ANSWER");
        }

        //make coroutine start 3s after being called
        yield return new WaitForSeconds(3f);
    }
}
