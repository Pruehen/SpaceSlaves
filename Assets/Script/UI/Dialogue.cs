using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{    
    public TextMeshProUGUI goTxt;
    public GameObject goNextBtn;
    public GameObject goSkipBtn;
    public GameObject goImageChar;

    float speed = 0.1f;
    float lockInpuT = 3.0f;

    public void SetSkip(bool isOn)
    {
        if (goSkipBtn == null)
            return;
        goSkipBtn.SetActive(isOn);
    }

    public void SetText(string text)
    {
        goTxt.text = text;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
