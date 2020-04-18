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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("StartGame"))
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }

}
