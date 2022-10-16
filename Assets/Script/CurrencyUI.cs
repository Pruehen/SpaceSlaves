using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI minText;
    public TextMeshProUGUI debriText;

    public void SetMinUI()
    {
        minText.text = CurrencyManager.instance.GetCurrency(CURRENCY_TYPE.Mineral).ToString();
    }
    public void SetDebriUI()
    {
        debriText.text = CurrencyManager.instance.GetCurrency(CURRENCY_TYPE.Debri).ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMinUI();
        SetDebriUI();

        InvokeRepeating("SetMinUI", 0, 0.1f);
        InvokeRepeating("SetDebriUI", 0, 1f);
    }
}
