using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LaborSceneManager : MonoBehaviour
{
    public void ToBattleScene()
    {
        SceneManager.LoadScene("DefenceScene");
    }

    public TextMeshProUGUI minText;
    void SetMinUI()
    {
        //minText.text = CurrencyManager.instance.cu
    }
}
