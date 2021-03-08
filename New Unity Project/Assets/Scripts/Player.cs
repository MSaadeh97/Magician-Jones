using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    static public bool isInArea = false;

    static public bool isSafe = false;

    static public bool isDead = false;

    static public bool isWinner = false;

    public bool trapTriggered = false;

    public LayerMask Area, WinArea;

    public TrapDoor trapScript;

    public Text timerText;
    public bool timerActive = false;
    float timeLeft = 45.0f;

    public Transform WayPoint;

    public BossMusic bossMusic;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            bossMusic.StopMusic();
            Time.timeScale = 0;
        }

        if (isSafe)
        {
            trapScript.StopDoor();
            timerActive = false;
        }

        if (bossMusic == null)
        {
            bossMusic = FindObjectOfType<BossMusic>();
        }

        if (timerText == null)
        {
            timerText = GameObject.Find("Timer").GetComponent<Text>();
        }

        if (timerActive)
        {
            timerText.gameObject.SetActive(true);
            StartCoroutine("Timer");
        }
        else
        {
            timerText.gameObject.SetActive(false);
        }

        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = new Vector3(WayPoint.position.x, WayPoint.position.y, WayPoint.position.z);
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Area"))
        {
            isInArea = true;
            bossMusic.PlayMusic();

            if (!trapTriggered)
            {
                trapScript.CloseDoor();
                timerActive = true;
                trapTriggered = true;
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("WinArea"))
        {
            isSafe = true;
            StopCoroutine("Timer");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Area"))
        {
            isInArea = false;
            bossMusic.StopMusic();
        }
    }

    IEnumerator Timer()
    {
        timeLeft -= Time.deltaTime;
        timerText.GetComponent<Text>().text = "Time Left: " + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            isDead = true;
            timeLeft = 0;
            StopCoroutine("Timer");
        }
        yield return null;
    }
}
