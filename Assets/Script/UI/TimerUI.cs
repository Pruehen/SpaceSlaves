using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TMP_Text timerTxt;
    public TextMeshProUGUI MaxText;
    public Image goIsMax;

    float FadeCool = 1f;
    float defaultFadeCool = 1f;

    bool isFade;

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
        goIsMax.gameObject.SetActive(isActive);

        isFade = isActive;
    }

    private void Update()
    {
        MaxEffect();
    }

    void MaxEffect()
    {
        Color color = goIsMax.color;

        if (isFade == true)
        {
            if (color.a <= 0)
            {
                FadeCool = defaultFadeCool;

                color.a = 1;
                goIsMax.color = color;

                MaxText.color = Color.white;
            }
            else
            {
                FadeCool = defaultFadeCool;

                color.a -= Time.deltaTime / defaultFadeCool;
                goIsMax.color = color;

                MaxText.color = new Color(MaxText.color.r, MaxText.color.g, MaxText.color.b, MaxText.color.a - Time.deltaTime / defaultFadeCool);

            }
        }
        else
        {
            return;
        }
    }
}