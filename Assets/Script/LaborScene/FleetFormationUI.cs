using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FleetFormationUI : MonoBehaviour
{
    public GameObject goQtySelect;    
    public Scrollbar goQtySlider;
    public TextMeshProUGUI goQtySliderTxt;

    public GameObject[] goFleets;
    public GameObject goSelectPop;

    int formIdx = 0;
    int idx = 0;
    int selAmount = 0;

    public void OnSelectFleet(int idx)
    {
        goSelectPop.SetActive(true);
        formIdx = idx;
    }
     
    public void OnSelectShip(int idx)
    {
        goQtySelect.SetActive(true);
        goQtySlider.value = 0;
        this.idx = idx;
    }
    public void OnConfirmShip()
    {
        goQtySelect.SetActive(false);
        goQtySlider.value = 0;
        FleetFormationManager.instance.SetUnit(idx, formIdx, selAmount);
    }
    public void OnEmptyShip()
    {
        goQtySelect.SetActive(false);
        goQtySlider.value = 0;
        FleetFormationManager.instance.RemoveUnit(formIdx);
    }

    // Update is called once per frame
    void Update()
    {
        if (goQtySelect.active)
        {
            var qty = FleetManager.instance.GetFleetQtyData(idx);
            selAmount = (int)Mathf.Floor(qty * goQtySlider.value);
            goQtySliderTxt.text = selAmount.ToString();
        }
        
        
    }
}
