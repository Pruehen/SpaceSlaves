using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DialogueData
{
    [TextArea(2,4)]
    public string text;
}

public class DialogueUI : MonoBehaviour
{
    public Dialogue goDial;
    public GameObject goBoard;
    bool isPlaying = false;

    public UnityEvent OnEndDial;

    [SerializeField]
    DialogueData[] presetDial;

    Queue<DialogueData> queueDial = new Queue<DialogueData>();

    // 수동으로 추가하기
    public void AddDial(string txt)
    {
        if (isPlaying)
            return;

        DialogueData data = new DialogueData();
        data.text = txt;

        queueDial.Enqueue(data);
    }
    
    public void Play(bool isSkipable)
    {
        if (isPlaying)
            return;
        // preset 이 우선 된다.
        if (presetDial.Length > 0)
        {
            foreach (var item in presetDial)
            {
                queueDial.Enqueue(item);
            }
        }
        isPlaying = true;

        gameObject.SetActive(true);
        goBoard.SetActive(true);

        goDial.Show();
        goDial.SetSkip(isSkipable);

        Next();
    }

    public void End()
    {
        isPlaying = false;
        gameObject.SetActive(false);
        goBoard.SetActive(false);

        goDial.Hide();
        queueDial.Clear();

        OnEndDial.Invoke();
    }

    public void Next()
    {
        if (!isPlaying)
            return;
        SoundManager.instance.clickSoundOn();

        if (queueDial.Count == 0)
        {
            End();
            return;
        }
        var dialData = queueDial.Dequeue();
        dialData.text = dialData.text.Replace("\\n", "\n");

        goDial.SetText(dialData.text);
    }
}
