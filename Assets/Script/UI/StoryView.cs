using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryView : MonoBehaviour
{
    public DialogueUI DUI;

    static bool isSkip = true;

    public List<GameObject> Arrow;

    public int ArrowCount = 0;
    public GameObject CImage;
    public List<Transform> CoverImage = new List<Transform>();

    public bool isTutorial = false;
    public GameObject TutoUI;

    public int count = 0;

    private void Awake()
    {
        isTutorial = false;
    }

    public void TutorialStart()
    {
        ArrowCount = 0;
        DUI.Play(isSkip);
        isTutorial = true;
        PlayerPrefs.SetInt("Tutorial", 1);
        TutoUI.SetActive(true);

        ButtonHighlighting();
    }

    public void ArrowPosCon()
    {
        if (Arrow[ArrowCount] != null)
        {
            if(isTutorial == true)
            {
                Arrow[ArrowCount].SetActive(true);

                if (ArrowCount > 0 && ArrowCount < Arrow.Count)
                {
                    Arrow[ArrowCount - 1].SetActive(false);
                }

                ArrowCount++;
            }         
        }
    }

    public void ArrowPosConSlider(Scrollbar slider)
    {
        if (Arrow[ArrowCount] != null)
        {
            if (isTutorial == true)
            {
                Arrow[ArrowCount].SetActive(true);

                if (ArrowCount > 0 && ArrowCount < Arrow.Count)
                {
                    Arrow[ArrowCount - 1].SetActive(false);
                }

                if (slider.value == 1)
                {
                    ArrowCount = 8;
                }
            }
        }
    }

    public void ArrowHide()
    {
        if(Arrow[ArrowCount] != null)
        {
            Arrow[ArrowCount].SetActive(false);
        }
    }

    void ButtonHighlighting()
    {
        if (isTutorial == true)
        {
            for (int i = 0; i < CImage.transform.childCount; i++)
            {
                CoverImage.Add(CImage.transform.GetChild(i));
            }   
        }
    }

    public void ButtonHighlightON()
    {
        if (Arrow[ArrowCount] != null)
        {
            if (isTutorial == true)
            {
                CoverImage[count].gameObject.SetActive(true);

                if (count > 0 && count < CoverImage.Count)
                {
                    CoverImage[count - 1].gameObject.SetActive(false);
                }

                count++;
            }
        }
    }

    public void ButtonHighlightONScroll(Scrollbar slider)
    {
        if (Arrow[ArrowCount] != null)
        {
            if (isTutorial == true)
            {
                CoverImage[count].gameObject.SetActive(true);

                if (count > 0 && count < CoverImage.Count)
                {
                    CoverImage[count - 1].gameObject.SetActive(false);
                }
            }
        }
    }

    public void ButtonHighlightOff(int count)
    {
        if(CoverImage[count] != null)
        {
            CoverImage[count].gameObject.SetActive(true);
        }
    }

    public void IsTutorialCheck(DialogueUI _DUI)
    {
        if(isTutorial == true)
        {
            _DUI.Play(isSkip);
        }
    }

    public void IsTutorialToggle(bool isT)
    {
        if (isT == true)
        {
            isTutorial = true;
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else
        {
            isTutorial = false;
            PlayerPrefs.DeleteKey("Tutorial");
            PlayerPrefs.DeleteKey("TutorialU");
            TutoUI.SetActive(false);
        }
    }

    public void BattleSceneTutorial(DialogueUI _DUI)
    {
        if(PlayerPrefs.HasKey("Tutorial") == true)
        {
            SoundManager.instance.clickSoundOn();
            Time.timeScale = 1;
            _DUI.Play(isSkip);
            PlayerPrefs.SetInt("TutorialU", 1);
        }
        else if(PlayerPrefs.HasKey("Tutorial") == false)
        {
            SoundManager.instance.clickSoundOn();
            Time.timeScale = 1;
            SceneManager.LoadScene("LaborScene");
        }
    }

    public void UpgradeSceneTutorial(DialogueUI _DUI)
    {
        if (PlayerPrefs.HasKey("Tutorial") == true && PlayerPrefs.HasKey("TutorialU") == true)
        {
            _DUI.Play(isSkip);
        }
    }
}
