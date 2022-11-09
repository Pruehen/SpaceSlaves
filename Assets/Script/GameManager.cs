using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public void SaveDataReset()
    {
        StageManager.instance.ResetStageData();
        CurrencyManager.instance.ResetSaveData();
        UpgradeManager.instance.ResetUpgradeData();
        FleetManager.instance.ResetFleetData();
        FleetFormationManager.instance.ResetFomationData();

        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void GameExit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public float bgmValue;
    public float soundValue;
    public float difficulty;

    public void SettingApplication()
    {
        SoundManager.instance.SoundSettingSet();
        BgmManager.instance.BgmSettingSet();
    }
}
