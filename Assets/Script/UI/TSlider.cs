using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TSlider : MonoBehaviour
{
    public StoryView storyView;

    public Scrollbar TScroll;
    public Scrollbar OScroll;

    private void Update()
    {
        SliderCon(storyView.isTutorial);
    }

    void SliderCon(bool isT)
    {
        if(isT == true && TScroll.value > 0)
        {
            TScroll.value = 1;

            OScroll.value = TScroll.value;
        }
    }
}
