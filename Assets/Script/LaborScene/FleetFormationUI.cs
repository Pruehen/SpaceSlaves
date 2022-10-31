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

    public FleetBarUI[] goFleets;
    public GameObject goSelectPop;

    int formIdx = 0;
    int idx = 0;
    int selAmount = 0;
    // 편집할 편대 선택
    public void OnSelectFleet(int idx)
    {
        goSelectPop.SetActive(true);
        SoundManager.instance.clickSoundOn();
        formIdx = idx;
    }
    //함선을 선택
    public void OnSelectShip(int idx)
    {
        goQtySelect.SetActive(true);
        SoundManager.instance.clickSoundOn();
        goQtySlider.value = 0;
        this.idx = idx;
    }
    public void OnConfirmShip()
    {
        goQtySelect.SetActive(false);
        SoundManager.instance.CloseSoundOn();
        goQtySlider.value = 0;
        FleetFormationManager.instance.SetUnit(idx, formIdx, selAmount);

        Refresh();
    }
    public void OnEmptyShip()
    {
        goQtySelect.SetActive(false);
        SoundManager.instance.CloseSoundOn();
        goQtySlider.value = 0;

        FleetFormationManager.instance.RemoveUnit(formIdx);
        Refresh();
    }
    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        RefreshFleetBar();
    }

    void RefreshFleetBar()
    {
        foreach (var item in goFleets)
        {
            var data = FleetFormationManager.instance.MakeValidateData();

            item.Refresh();
            item.SetWarningIcon(data.ProbFleetIdx.Contains(item.id));            
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (goQtySelect.active)
        {
            var qty = FleetManager.instance.GetFleetQtyData(idx) - FleetFormationManager.instance.GetShipQty(idx);
            qty = Mathf.Max(qty, 0);

            selAmount = (int)Mathf.Floor(qty * goQtySlider.value);
            goQtySliderTxt.text = selAmount.ToString();
        }  
    }
}
