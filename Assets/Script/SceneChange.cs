using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject Message;

    public void GoLaborScene()
    {
        Time.timeScale = 1;
        SoundManager.instance.clickSoundOn();
        SceneManager.LoadScene("LaborScene");
    }
    public void GoDefenceScene()
    {
        SoundManager.instance.clickSoundOn();
        if (!FleetFormationManager.instance.CheckValidateData())
        {
            //MessageManager.instance.PopupOk("�Լ��� �����Ͽ� ���� �� �� �����ϴ�.");
            Message.SendMessage("MessageQ", "�Լ��� �����Ͽ� ���� �� �� �����ϴ�.");
            return;
        }

        Time.timeScale = 1;
        LaborSceneManager.instance.SettingApplication();
        SceneManager.LoadScene("DefenceScene");
    }

    public void GoUpgradeScene()
    {
        Time.timeScale = 1;
        SoundManager.instance.clickSoundOn();
        LaborSceneManager.instance.SettingApplication();
        SceneManager.LoadScene("UpgradeScene");
    }
}
