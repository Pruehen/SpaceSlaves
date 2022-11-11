using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    // 원래는 sceneChange에 있었으나 부적절해 보여 이곳으로 옮기기로 했음
    public GameObject SettingWin;

    private void Start()
    {
        SettingWin.SetActive(false);
    }

    public void SettingsOn()
    {
        Time.timeScale = 0;
        SettingWin.gameObject.SetActive(true);
    }

    public void SettingOff()
    {
        Time.timeScale = 1;
        SettingWin.gameObject.SetActive(false);
    }
}
