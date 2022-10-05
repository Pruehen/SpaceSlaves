using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LaborSceneManager : MonoBehaviour
{
    public static LaborSceneManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void ToBattleScene()
    {
        SceneManager.LoadScene("DefenceScene");
    }

    public TextMeshProUGUI minText;
    public void SetMinUI()
    {
        minText.text = CurrencyManager.instance.GetCurrency(CURRENCY_TYPE.Mineral).ToString();
    }

    public GameObject buildWdw;
    public void BuildWdwTogle(bool value)
    {
        buildWdw.SetActive(value);
    }
}
