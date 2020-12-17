using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 15;
    public bool takingAway = false;

    void start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    void Update()
    {
        if(takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        } else if (secondsLeft == 0){
            Time.timeScale = 0;
            textDisplay.GetComponent<Text>().text = "GAME OVER";
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;

        }
        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
            takingAway = false;
        }

    }
}
