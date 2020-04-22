using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingDDOL : MonoBehaviour
{
    static TeleportingDDOL instance;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
