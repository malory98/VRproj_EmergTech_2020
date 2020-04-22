using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*******************************
 Name: Lyssa Tino
 Course: Emerging Technologies
 Project: Wake Up
*******************************/

public class SceneChange : MonoBehaviour
{
    public string sceneName;

    public GameObject playerHUD;
    public GameObject testingHUD;

    public AudioSource fail;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("StartGame"))
        {
            SceneManager.LoadScene(sceneName);
            playerHUD.SetActive(true);
            testingHUD.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("Incorrect"))
        {
            fail.Play();
        }
        
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

}
