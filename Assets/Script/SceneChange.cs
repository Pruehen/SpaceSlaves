using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject Message;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitButton();
        }
    }

    public void GoLaborScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LaborScene");
    }
    public void GoDefenceScene()
    {
        if (!FleetFormationManager.instance.CheckValidateData())
        {
            MessageManager.instance.PopupOk("함선이 부족하여 진행 할 수 없습니다.");
            return;
        }

        Time.timeScale = 1;
        SceneManager.LoadScene("DefenceScene");
    }

    public void GoUpgradeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("UpgradeScene");
    }

    public void ExitButton()
    {        
        //Message.SendMessage("MessageQ","종료하시겠습니까?");
        Application.Quit();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }

}
