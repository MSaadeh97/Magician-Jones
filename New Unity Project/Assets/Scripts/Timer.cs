using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;

    public bool timerActive = false;

    float timeLeft = 59.0f;

    /*
    private void OnEnable()
    {
        timerActive = false;
        timeLeft = 59.0f;
    }
    */

    void Update()
    {
        if (timerActive)
        {
            timerText.gameObject.SetActive(true);
            StartCoroutine("DoorTimer");
        }
    }

    IEnumerator DoorTimer()
    {
        yield return new WaitForSeconds(1);
        timeLeft -= Time.deltaTime;
        timerText.GetComponent<Text>().text = "Time Left: " + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            KillPlayer();
            timeLeft = 0;
            StopCoroutine("DoorTimer");
        }
        yield return null;
    }

    void KillPlayer()
    {
        Player.isDead = true;
    }

    public void ActivateTimer()
    {
        timerActive = true;
    }

    public void ResetTimer()
    {
        timeLeft = 59;
        timerActive = false;
        timerText.gameObject.SetActive(false);
    }
}
