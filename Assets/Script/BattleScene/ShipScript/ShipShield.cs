using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShield : MonoBehaviour
{
    public GameObject shield;
    /*MeshRenderer shieldRdr;
    Color shieldColor;
    float alpha;

    private void Start()
    {
        shieldRdr = shield.GetComponent<MeshRenderer>();
        shieldColor = shieldRdr.material.color;
    }

    private void Update()
    {
        shieldRdr.material.color = new Color(shieldColor.r, shieldColor.g, shieldColor.b, alpha);
        if (alpha > 0)
        {
            alpha--;
        }
    }*/

    private void Start()
    {
        shield.SetActive(false);
    }

    public void EffectOn()
    {
        //alpha = 80;

        shield.SetActive(true);
        StartCoroutine(EffectOff());
    }

    IEnumerator EffectOff()
    {
        yield return new WaitForSeconds(0.3f);

        shield.SetActive(false);
    }
}
