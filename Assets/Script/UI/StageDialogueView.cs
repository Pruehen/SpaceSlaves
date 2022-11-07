using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDialogueView : MonoBehaviour
{
    public DialogueUI DUI;
    bool isSkip = true;
    public List<DialogueUI> DUISet;

    private void Start()
    {
        StageDetect();
        if(DUI != null)
        {
            DUI.Play(isSkip);
        }     
    }

    void StageDetect()
    {
        switch (StageManager.instance.selectedStage)
        {
            case 1:
                Time.timeScale = 0;
                DUI = DUISet[0];
                break;
            case 3:
                Time.timeScale = 0;
                DUI = DUISet[2];
                break;
            case 4:
                Time.timeScale = 0;
                DUI = DUISet[3];
                break;
            case 6:
                Time.timeScale = 0;
                DUI = DUISet[5];
                break;
        }
    }

    public void BattleSceneStory()
    {
        switch (StageManager.instance.selectedStage)
        {
            case 2:
                Time.timeScale = 0;
                DUI = DUISet[1];
                break;
            case 5:
                Time.timeScale = 0;
                DUI = DUISet[4];
                break;
            case 7:
                Time.timeScale = 0;
                DUI = DUISet[6];
                break;
        }
    }

    public void DialogueEnd()
    {
        Time.timeScale = 1;
    }
}
