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

    public void ShipHP(float hp, float sd, bool ishit)
    {
        if (ishit == true)
        {
            HPbar.gameObject.SetActive(true);
            SDbar.gameObject.SetActive(true);

            HPBar.fillAmount = hp * 0.01f;
            SDBar.fillAmount = sd * 0.01f;

            Debug.Log(HPBar.fillAmount + SDBar.fillAmount);

            Cooling = true;
        }
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
