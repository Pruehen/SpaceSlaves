using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgTmp : MonoBehaviour
{
    Transform targetTrf;

    Color32 shieldHitColor = new Color32(125, 230, 255, 255);    
    Color32 armorDefenceColor = new Color32(150, 150, 150, 255);
    Color32 normalColor = new Color32(255, 255, 255, 255);

    float startFontSize;
    TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI = this.GetComponent<TextMeshProUGUI>();
        startFontSize = textMeshProUGUI.fontSize;
        this.gameObject.SetActive(false);
    }

    public void Set(Transform targetTrf, int text, DmgTextType dmgTextType)
    {
        this.targetTrf = targetTrf;
        this.transform.position = Camera.main.WorldToScreenPoint(this.targetTrf.position);
        textMeshProUGUI.text = text.ToString();
        textMeshProUGUI.fontSize = startFontSize + Mathf.Log10(text) * 2;

        this.gameObject.SetActive(true);
        Invoke("PushPool", 0.75f);
        count = 0;
        
        if(dmgTextType == DmgTextType.ShieldHit)
        {
            textMeshProUGUI.color = shieldHitColor;
        }
        else if(dmgTextType == DmgTextType.ShieldBreake)
        {
            textMeshProUGUI.color = shieldHitColor;
        }
        else if(dmgTextType == DmgTextType.ArmorHit)
        {
            textMeshProUGUI.color = normalColor;
        }
        else if(dmgTextType == DmgTextType.ArmorDefence)
        {
            textMeshProUGUI.color = armorDefenceColor;
        }
    }

    float count;

    private void Update()
    {
        if(this.gameObject.activeSelf)
        {
            count += Time.deltaTime;
            this.transform.position = Camera.main.WorldToScreenPoint(this.targetTrf.position) + new Vector3(0, count * 30, 0);
            textMeshProUGUI.fontSize *= 0.999f;
        }
    }

    void PushPool()
    {
        this.gameObject.SetActive(false);
    }
}

public enum DmgTextType
{
    ShieldHit,
    ShieldBreake,
    ArmorHit,
    ArmorDefence
}
