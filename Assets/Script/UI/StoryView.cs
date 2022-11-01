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
        DUI.Play(isSkip);
        isTutorial = true;
    }

    public void ArrowPosCon()
    {
        if (Arrow[ArrowCount] != null)
        {
            Arrow[ArrowCount].SetActive(true);

            if (ArrowCount > 0)
            {
                Arrow[ArrowCount - 1].SetActive(false);
            }

            ArrowCount++;
        }
    }

    public void ButtonHighlighting(int count)
    {
        CoverImage.Add(CImage.transform.GetChild(count));

        CoverImage[count].gameObject.SetActive(true);
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
