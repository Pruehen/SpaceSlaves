using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    // 원래는 sceneChange에 있었으나 부적절해 보여 이곳으로 옮기기로 했음
    public GameObject SettingWin;

    public void SettingsOn()
    {
        SettingWin.SetActive(true);
    }

    public void SettingOff()
    {
        SettingWin.SetActive(false);
    }
}
