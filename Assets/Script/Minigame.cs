using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
    public GameObject btn;

    public GameObject pointOne;
    public GameObject poinTwo;

    public GameObject SpinOne;
    public GameObject SpinTwo;

    public GameObject ResultPop;
    public GameObject ResultWIN;
    public GameObject ResultFAIL;
    public TMPro.TMP_Text rewardPoptxt;
    public TMPro.TMP_Text attempsTxt;

    public int attempts = 3;

    public Image goObSti_Blocker;
    public int rewardM_Amount = 1000;
    public int rewardD_Amount = 1000;

    public TimeGatedObject timegate;

    AudioSource audi;
    public AudioClip seWrong;
    public AudioClip seDingdong;

    // the winner is...
    bool isGameOver = false;
    bool isSuccesseAble = false;
    bool isSpinActive = false;
    private void Start()
    {
        audi = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GameStart();
    }
    public void Shoot()
    {
        if (isGameOver || !isSpinActive)
            return;
        isGameOver = isSuccesseAble;

        StopSpinner();

        if (isSuccesseAble)
        {
            OnSuccesse();
        }
        else // fail
        {
            if (attempts-- <= 0)
                OnFailed();
            else 
                RefreshUI();

            Invoke("PlaySpinner", 1);
        }
    }

    void PlaySpinner()
    { 
        isSpinActive = true;
    }   

    void StopSpinner()
    {
        isSpinActive = false;
    }

    void Reward()
    {
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Mineral, rewardM_Amount);
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Debri, rewardD_Amount);
        Debug.Log("Minigame Bonus " + string.Format("{0}/{1}", rewardM_Amount, rewardD_Amount));
        Destroy(btn, 1f);
    }

    void OnSuccesse()
    {
        audi.PlayOneShot(seDingdong);
        OnResult();        
        ResultWIN.SetActive(true);
        rewardPoptxt.text = string.Format("±¤¹°{0} / ÀÜÇØ{1}", rewardM_Amount, rewardD_Amount);
        Invoke("Reward", 2.50f);
    }

    void OnFailed()
    {
        audi.PlayOneShot(seWrong);
        OnResult();
        ResultFAIL.SetActive(true);
        Destroy(btn, 2.50f); 
    }
    void OnResult()
    {
        if(timegate)
            timegate.CheckGate();

        isGameOver = true;

        RefreshUI();
        ResultPop.SetActive(true);

        ResultWIN.SetActive(false);
        ResultFAIL.SetActive(false);
    }

    void RefreshUI()
    {
        attempsTxt.text = attempts.ToString();
    }

    void GameStart()
    {
        // init
        goObSti_Blocker.fillAmount = Random.Range(0.2f, 0.4f);
        goObSti_Blocker.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        var spinerStartVal = Quaternion.Euler(0, 0, Random.Range(0, 360));
        SpinOne.transform.rotation = spinerStartVal;
        SpinTwo.transform.rotation = spinerStartVal;


        isGameOver = false;
        isSpinActive = false;
        isSuccesseAble = false;

        RefreshUI();

        Invoke("PlaySpinner", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;
        float minDist = 50;
        var dist = Vector3.Distance(poinTwo.transform.position, pointOne.transform.position);
        isSuccesseAble = dist < minDist;

        if (isSpinActive)
        {
            float spinSpeed = 200;
            SpinOne.transform.Rotate(Vector3.back, spinSpeed * Time.deltaTime);
            SpinTwo.transform.Rotate(Vector3.back, -spinSpeed * Time.deltaTime);

            goObSti_Blocker.color = new Color(1, 1, 1, 1);
        }
        else
        {
            goObSti_Blocker.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
