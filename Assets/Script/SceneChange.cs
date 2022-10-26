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
            MessageManager.instance.PopupOk("�Լ��� �����Ͽ� ���� �� �� �����ϴ�.");
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
        //Message.SendMessage("MessageQ","�����Ͻðڽ��ϱ�?");
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
