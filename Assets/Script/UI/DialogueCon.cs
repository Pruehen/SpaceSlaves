using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCon : MonoBehaviour
{
    DialogueUI Dui;
    public GameObject StartT;
    bool isSkip;

    private void Start()
    {
        Dui.Play(isSkip);
    }
}
