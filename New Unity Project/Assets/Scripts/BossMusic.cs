using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    AudioSource bossMusic;
    void Start()
    {
        bossMusic = GetComponent<AudioSource>();
    }
    void Update()
    {

    }

    public void PlayMusic()
    {
        bossMusic.Play();
    }

    public void StopMusic()
    {
        bossMusic.Pause();
    }
}
