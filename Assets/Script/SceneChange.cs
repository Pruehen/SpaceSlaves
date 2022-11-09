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
            //MessageManager.instance.PopupOk("함선이 부족하여 진행 할 수 없습니다.");
            Message.SendMessage("MessageQ", "함선이 부족하여 진행 할 수 없습니다.");
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
