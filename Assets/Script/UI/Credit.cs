using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    public GameObject creditView;
    public GameObject credit;
    bool IsCredit;
    float speed = 1.5f;

    void Update()
    {
        if (IsCredit == true)
        {
            credit.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if(IsCredit == false)
        {
            ResetCredit();
        }
    }

    void ResetCredit()
    {
        credit.transform.position = new Vector3(0, 0, 0);
    }

    public void CreditCheck(bool isCredit)
    {
        creditView.SetActive(isCredit);
        IsCredit = isCredit;
    }
}
