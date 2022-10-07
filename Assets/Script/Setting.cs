using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    // ������ sceneChange�� �־����� �������� ���� �̰����� �ű��� ����
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
