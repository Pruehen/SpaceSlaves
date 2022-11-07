using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeScaleCon : MonoBehaviour
{
    float count = 1f;

    public TextMeshProUGUI TimeScaleText = null;

    public void TimeScaleUP()
    {
        if(count < 2.5)
        {
            count = count + 0.5f;
            TimeScaleText.text = "" + count;
        }
        else if(count == 2.5)
        {
            count = 1f;
            TimeScaleText.text = "" + count;
        }

        Time.timeScale = count;
    }
}
