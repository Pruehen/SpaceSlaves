using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    // ������ sceneChange�� �־����� �������� ���� �̰����� �ű��� ����
    public GameObject SettingWin;
    float TimeScaleSave = 0;

    private void Start()
    {
        SettingWin.SetActive(false);
    }

    public void SettingsOn()
    {
        TimeScaleSave = Time.timeScale;
        Time.timeScale = 0;
        SettingWin.gameObject.SetActive(true);
    }

    public void SettingOff()
    {
        Time.timeScale = TimeScaleSave;
        SettingWin.gameObject.SetActive(false);
    }
}
