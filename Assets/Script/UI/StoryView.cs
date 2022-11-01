using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryView : MonoBehaviour
{
    public DialogueUI DUI;
    public DialogueUI DUI_;

    static bool isSkip = true;

    public List<GameObject> Arrow;

    public int ArrowCount = 0;
    public GameObject CImage;
    public List<Transform> CoverImage = new List<Transform>();

    public bool isTutorial = false;

    public void TutorialStart()
    {
        ArrowCount = 0;
        DUI.Play(isSkip);
        isTutorial = true;
    }

    public void ArrowPosCon()
    {
        Debug.Log(ArrowCount);

        if (Arrow[ArrowCount] != null)
        {
            if(isTutorial == true)
            {
                Debug.Log(ArrowCount);

                Arrow[ArrowCount].SetActive(true);

                if (ArrowCount > 0)
                {
                    Arrow[ArrowCount - 1].SetActive(false);
                }

                ArrowCount++;
            }         
        }
    }

    public void ButtonHighlighting(int count)
    {
        for(int i = 0; i < CImage.transform.childCount; i++)
        {
            CoverImage.Add(CImage.transform.GetChild(i));
        }
        Debug.Log(isTutorial);
        if (isTutorial == true)
        {
            CoverImage[count].gameObject.SetActive(true);
        }
    }

    public void ButtonHighlightingOff(int count)
    {
        CoverImage[count].gameObject.SetActive(false);
    }

    public void IsTutorialCheck()
    {
        if(isTutorial == true)
        {
            DUI_.Play(isSkip);
        }
    }

    public void IsTutorialToggle()
    {
        isTutorial = false;
    }
}
