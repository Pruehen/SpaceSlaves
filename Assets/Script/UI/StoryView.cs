using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryView : MonoBehaviour
{
    public DialogueUI DUI;

    static bool isSkip = true;

    public List<GameObject> Arrow;

    public int ArrowCount = 0;

    private void Awake()
    {
        DUI.Play(isSkip);
    }

    private void Start()
    {
        Destroy(DUI.gameObject);
    }

    public void ArrowPosCon()
    {
        if (Arrow[ArrowCount] != null)
        {
            Arrow[ArrowCount].gameObject.SetActive(true);

            ArrowCount++;

            Debug.Log(ArrowCount);
        }

        if (ArrowCount > 0)
        {
            Arrow[ArrowCount - 1].gameObject.SetActive(false);
        }
    }
}
