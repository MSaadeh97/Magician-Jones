using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinObject : MonoBehaviour
{
    public WinSound winNoise;

    public Golem golem;

    void Start()
    {
        winNoise = FindObjectOfType<WinSound>();
        golem = FindObjectOfType<Golem>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 0;
            Player.isWinner = true;
            winNoise.PlaySound();
            golem.StopAS();
            Destroy(gameObject);
        }
    }
}
