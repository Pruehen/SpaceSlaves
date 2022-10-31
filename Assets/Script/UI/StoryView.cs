using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryView : MonoBehaviour
{
    public DialogueUI DUI;

    static bool isSkip = true;

    public List<GameObject> Arrow;

    public int ArrowCount = 0;
    public GameObject CImage;
    public List<Transform> CoverImage = new List<Transform>();

    private void Awake()
    {
        DUI.Play(isSkip);
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

            Debug.Log(ArrowCount);
        }
    }

    public void ButtonHighlighting(int count)
    {
        CoverImage.Add(CImage.transform.GetChild(count));

        CoverImage[count].gameObject.SetActive(true);
    }
}
