using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarControl : MonoBehaviour
{
    public ShipControl shipCon;
    public Image HPBar;
    public Image SDBar;
    public GameObject HPbar;
    public GameObject SDbar;
    float Cool = 1.5f;
    float MaxCool = 1.5f;
    bool Cooling = false;

    private void Update()
    {
        Fade();
    }

    public void ShipHP(float hp, float sd, float maxHp, float maxSd)
    {

        HPbar.gameObject.SetActive(true);
        SDbar.gameObject.SetActive(true);

        SDBar.fillAmount = hp/ maxHp;
        HPBar.fillAmount = sd/maxSd;

        Debug.Log(HPBar.fillAmount + SDBar.fillAmount);

        Cooling = true;
    }

    void Fade()
    {
        if (Cooling == true)
        {
            Cool = Cool - Time.deltaTime;

            if (Cool <= 0)
            {
                Cool = MaxCool;
                Cooling = false;
                HPbar.gameObject.SetActive(false);
                SDbar.gameObject.SetActive(false);
            }
        }
    }
}
