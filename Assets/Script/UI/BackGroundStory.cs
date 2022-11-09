using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundStory : MonoBehaviour
{
    public DialogueUI DUI;
    bool isSkip = false;
    float MCooltime = 2f;
    float DefaultMCooltime = 2f;

    void Start()
    {
        DUI.Play(isSkip);
    }

    private void Update()
    {
        MCool();
    }

    void MCool()
    {
        MCooltime -= Time.deltaTime;

        if(MCooltime <= 0)
        {
            MCooltime = DefaultMCooltime;
            DUI.Next();
        }
    }
}
