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
    public Transform CImage;
    public List<GameObject> CoverImage = new List<GameObject>();

    public GameObject Tuto;
    public GameObject UTuto;
    public bool isTutorial = false;

    int count = 0;

    private void Awake()
    {
        isTutorial = false;

       // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Tutorial") == true && Tuto != null)
        {
            Tuto.SetActive(true);
            if(PlayerPrefs.HasKey("UTutorial") == true&& UTuto != null)
            UTuto.SetActive(true);
        }
        else if(PlayerPrefs.HasKey("Tutorial") == false)
        {
            Tuto.SetActive(false);
            if (UTuto != null)
            {
                UTuto.SetActive(false);
            }
        }
    }

    public void TutorialStart()
    {
        ArrowCount = 0;
        DUI.Play(isSkip);
        isTutorial = true;
        PlayerPrefs.SetInt("Tutorial", 1);

        Tuto.SetActive(true);       
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
        if(Arrow[ArrowCount - 1] != null)
        {
            Arrow[ArrowCount - 1].SetActive(false);
        }
    }

    public void ButtonHighlighting()
    {
        if (isTutorial == true && CoverImage != null)
        {
            CoverImage[count].gameObject.SetActive(true);

            if (count > 0 && count < CoverImage.Count)
            {
                CoverImage[count - 1].SetActive(false);
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

    public void ButtonHighlightingOff()
    {
        if (CoverImage[count - 1] != null)
        {
            //Debug.Log("dajkhfdk");
            CoverImage[count - 1].gameObject.SetActive(false);
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
        }
    }

    public void BattleSceneTutorial(DialogueUI _DUI)
    {
        if(PlayerPrefs.HasKey("Tutorial"))
        {
            SoundManager.instance.clickSoundOn();
            _DUI.Play(isSkip);
            PlayerPrefs.SetInt("UTutorial", 1);
        }
        else
        {
            SoundManager.instance.clickSoundOn();
            SceneManager.LoadScene("LaborScene");
        }
    }

    public void UpgradeSceneTutorial(DialogueUI _DUI)
    {
        if (PlayerPrefs.HasKey("Tutorial") && PlayerPrefs.HasKey("UTutorial"))
        {
            _DUI.Play(isSkip);
            PlayerPrefs.DeleteAll();
        }
    }

    public void TutoOff()
    {
        Tuto.SetActive(false);
        UTuto.SetActive(false);
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
