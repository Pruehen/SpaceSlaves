using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject SettingWin;

    public void GoLaborScene()
    {
        SceneManager.LoadScene("LaborScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SettingsOn()
    {
        SettingWin.SetActive(true);
    }

    public void SettingOff()
    {
        SettingWin.SetActive(false);
    }
}
