using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSound : MonoBehaviour
{
    AudioSource winSound;
    void Start()
    {
        winSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        
    }

    public void PlaySound()
    {
        winSound.Play();
    }
}
