using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDialogueView : MonoBehaviour
{
    private void Start()
    {
        StageDetect();
    }

    void StageDetect()
    {
        switch (StageManager.instance.selectedStage)
        {
            case 0:
                Debug.Log("1");
                break;
            case 1:
                Debug.Log("2");
                break;
            case 2:
                Debug.Log("3");
                break;
            case 3:
                Debug.Log("4");
                break;
            case 4:
                Debug.Log("5");
                break;
            case 5:
                Debug.Log("6");
                break;
        }
    }

    void Dialogue()
    {

    }
}
