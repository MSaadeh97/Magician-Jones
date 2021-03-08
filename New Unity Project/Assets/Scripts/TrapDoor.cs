using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapDoor : MonoBehaviour
{
    public Transform startPosition, endPosition;

    public float speed;

    public bool trapTriggered = false;

    AudioSource doorDrop;

    void Start()
    {
        doorDrop = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if(trapTriggered)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, step);
        }

        if(Vector3.Distance(transform.position, endPosition.position) < 0.001f)
        {
            trapTriggered = false;
            //door closed
        }
    }

    public void CloseDoor()
    {
        trapTriggered = true;
        doorDrop.Play();
    }

    public void StopDoor()
    {
        trapTriggered = false;
        doorDrop.Stop();
    }
}

