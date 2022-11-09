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


    /// 이 아래로 <Setting>
    /// 게임 전반적으로 사용되는 value들이 있는 파일
    public float bgmValue;
    public float soundValue;

    // 게임 난이도
    public float difficulty;

    public void SettingApplication()
    {
        SoundManager.instance.SoundSettingSet();
        BgmManager.instance.BgmSettingSet();
    }
}
