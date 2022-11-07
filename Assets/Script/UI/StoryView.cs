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

    int count = 0;

    private void Awake()
    {
        isTutorial = false;

       // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //UpgradeSceneTutorial(DUI);
    }

    public void TutorialStart()
    {
        ArrowCount = 0;
        DUI.Play(isSkip);
        isTutorial = true;
        PlayerPrefs.SetInt("Tutorial", 1);

        for (int i = 0; i < CImage.transform.childCount; i++)
        {
            CoverImage[i] = CImage.transform.GetChild(i);
        }
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
                    ArrowCount++;
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

    public void ButtonHighlighting()
    {
        if (isTutorial == true)
        {
            CoverImage[count].gameObject.SetActive(true);

            if (count > 0 && count < CoverImage.Count)
            {
                Arrow[count - 1].SetActive(false);
            }

            count++;
        }
    }

    public void ButtonHighlightingSlider(Scrollbar slider)
    {
        if (isTutorial == true)
        {
            CoverImage[count].gameObject.SetActive(true);

            if (count > 0 && count < CoverImage.Count)
            {
                Arrow[count - 1].SetActive(false);
            }

            if (slider.value == 1)
            {
                count++;
            }
        }
    }

    public void ButtonHighlightingOff(int count)
    {
        CoverImage[count].gameObject.SetActive(false);
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
        }
    }

    public void BattleSceneTutorial(DialogueUI _DUI)
    {
        if(PlayerPrefs.HasKey("Tutorial"))
        {
            SoundManager.instance.clickSoundOn();
            _DUI.Play(isSkip);
        }
        else
        {
            SoundManager.instance.clickSoundOn();
            SceneManager.LoadScene("LaborScene");
        }
    }

    public void UpgradeSceneTutorial(DialogueUI _DUI)
    {
        if (PlayerPrefs.HasKey("Tutorial"))
        {
            _DUI.Play(isSkip);
        }
    }
}
