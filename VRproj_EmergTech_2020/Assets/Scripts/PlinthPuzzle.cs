﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*******************************
 Name: Lyssa Tino
 Course: Emerging Technologies
 Project: Wake Up
*******************************/

public class PlinthPuzzle : MonoBehaviour
{

    public string correctItemName;
    public GameObject door;
    public GameManager gm;

    public AudioSource objectPlacedAS;
    public AudioSource puzzleFailAS;
    public AudioSource puzzleRightAnswer;

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
            //door.SetActive(false);
            FadeDoor();
            gm.PassLevel();
            Debug.Log("CORRECT ANSWER");
            objectPlacedAS.Play();
        }
        else if (collision.gameObject.CompareTag("Incorrect"))
        {
            objectPlacedAS.Play();
            gm.LoseLife();
            Debug.Log("WRONG ANSWER");
            StartCoroutine(PuzzleFail());
        }

        //make coroutine start 3s after being called
        yield return new WaitForSeconds(3f);
    }

    //fades out door
    public void FadeDoor()
    {
        Color objectColor = door.GetComponent<Renderer>().material.color;
        float fadeAmount = objectColor.a - (5 * Time.deltaTime);
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        door.GetComponent<Renderer>().material.color = objectColor;

        puzzleRightAnswer.Play();

        StartCoroutine(TurnOffDoor());
    }


    //turns off door after fade
    IEnumerator TurnOffDoor()
    {
        door.SetActive(false);

        //make coroutine start 5s after being called
        yield return new WaitForSeconds(5f);
    }


    //Play Puzzle Fail SFX
    IEnumerator PuzzleFail()
    {
        yield return new WaitForSeconds(1f);

        puzzleFailAS.Play();
    }


}
