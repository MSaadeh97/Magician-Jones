using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartVariable : MonoBehaviour
{
    public bool restart = false;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("RestartVar");

        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    
    public void SetTrue()
    {
        restart = true;
    }

    public void SetFalse()
    {
        restart = false;
    }
}
