using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TMP_Text timerTxt;
    public GameObject goIsMax;

    // Update is called once per frame
    public void UpdateTimer(long ticks)
    {
        TimeSpan timesp = new TimeSpan(ticks);
        timerTxt.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timesp.Hours, timesp.Minutes, timesp.Seconds);
    }
    public void SetAvail(bool isAvaile)
    {
        timerTxt.color = isAvaile ? Color.white : Color.red;
    }

    public void SetMax(bool isActive)
    {
        goIsMax.SetActive(isActive);
    }
}