using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShield : MonoBehaviour
{
    public GameObject shield;
    bool fading = false;

    MeshRenderer MyRenderer;

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
        MyRenderer = shield.GetComponent<MeshRenderer>();
    }

    public void EffectOn()
    {
        //alpha = 80;

        shield.SetActive(true);
        //StartCoroutine(EffectOff());
        //Fade(true, 3f); //Fade In
        Fade(false, 0.4f);//Fade Out
    }


    void Fade(bool fadeIn, float duration)
    {
        if (fading)
        {
            return;
        }
        fading = true;

        changeModeToFade();
        StartCoroutine(FadeTo(fadeIn, duration));
    }

    void changeModeToFade()
    {
        MyRenderer.material.SetFloat("_Mode", 2);
        MyRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        MyRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        MyRenderer.material.SetInt("_ZWrite", 0);
        MyRenderer.material.DisableKeyword("_ALPHATEST_ON");
        MyRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        MyRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        MyRenderer.material.renderQueue = 3000;
    }

    IEnumerator FadeTo(bool fadeIn, float duration)
    {
        //MyRenderer.material.
        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0;
            b = 1;
        }
        else
        {
            a = 1;
            b = 0;
        }


        //Enable MyRenderer component
        if (!MyRenderer.enabled)
            MyRenderer.enabled = true;

        //Get original Mesh Color
        Color meshColor = MyRenderer.material.color;


        //Do the actual fading
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);
            //Debug.Log(alpha);


            MyRenderer.material.color = new Color(meshColor.r, meshColor.g, meshColor.b, alpha);
            yield return null;
        }

        if (!fadeIn)
        {
            //Disable Mesh Renderer
            MyRenderer.enabled = false;
        }
        fading = false; //So that we can call this function next time

        shield.SetActive(false);
    }
}
