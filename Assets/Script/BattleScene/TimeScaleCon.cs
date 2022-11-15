using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeScaleCon : MonoBehaviour
{
    //public CameraControl CamCon;

    //public bool isClicked;

    public float count = 1f;

    public TextMeshProUGUI TimeScaleText = null;

    public void TimeScaleUP()
    {
        //isClicked = true;

        if (count < 2.5)
        {
            count = count + 0.5f;
            TimeScaleText.text = "" + count;

            //CamCon.dragSpeed /= count;
        }
        else if(count == 2.5)
        {
            //CamCon.dragSpeed *= count;

            count = 1f;
            TimeScaleText.text = "" + count;
        }

        Time.timeScale = count;

        //Debug.Log(CamCon.dragSpeed);

        //isClicked = false;
    }
}
