using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCheck : MonoBehaviour
{
    public List<Image> StageLock = new List<Image>();
    public GameObject SystemMessage;

    private void Start()
    {
        ImageColorSet();
    }

    void ImageColorSet()
    {
        for(int i = 0; i< StageLock.Count; i++)
        {
            Color color = StageLock[i].color;
            color = new Color(0.5f, 0f, 0f, 0.7f);
            StageLock[i].color = color;
        }
    }

    public void StageClearCheck()
    {
        for (int i = 0; i < StageManager.STAGE_COUNT; i++)
        {
            if( i > 0)
            {
                if (StageManager.instance.GetStageData(i - 1).isClear == true && i > 0)
                {
                    StageLock[i].gameObject.SetActive(false);
                }
                else
                {
                    StageLock[i].gameObject.SetActive(true);
                }
            }
            else if( i == 0)
            {
                StageLock[i].gameObject.SetActive(false);
            }
        }
    }

    public void ClearNotice(int value)
    {
        if (value > 0)
        {
            if (StageManager.instance.GetStageData(value - 1).isClear == true && value > 0)
            {
                LaborSceneManager.instance.StageInfoWdwToggle(value);
            }
            else
            {
                SystemMessage.SendMessage("MessageQFade", "이전 스테이지 클리어가 필요합니다.");
            }
        }
        else if (value == 0)
        {
            LaborSceneManager.instance.StageInfoWdwToggle(value);
        }
    }
}
