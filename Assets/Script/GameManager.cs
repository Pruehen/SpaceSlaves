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


    /// �� �Ʒ��� <Setting>
    /// ���� ���������� ���Ǵ� value���� �ִ� ����
    public float BGMValue
    {
        set;
        get;
    }
    public float SEValue
    {
        set;
        get;
    }

    // ���� ���̵�
    // 1 ���Ϸ� ���� ������ ������ ����
    public float difficulty = 1;

    public float Difficulty
    {
        set {
            difficulty = Mathf.Max(value, 1);
        }
        get 
        {
            return difficulty;
        }
    }

    public void SettingApplication()
    {
        SoundManager.instance.SoundSettingSet();
        BgmManager.instance.BgmSettingSet();
    }


    // ����
    public void SaveSettings()
    {
        
    }
    public void LoadSettings()
    {
        
    }
}
