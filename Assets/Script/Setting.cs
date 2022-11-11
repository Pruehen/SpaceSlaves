using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    // ������ sceneChange�� �־����� �������� ���� �̰����� �ű��� ����
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
