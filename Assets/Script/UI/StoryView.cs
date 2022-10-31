using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryView : MonoBehaviour
{
    public DialogueUI DUI;

    static bool isSkip = true;

    public GameObject SelectBox;
    public GameObject YesB;
    public List<GameObject> Arrow;

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

    }
}
