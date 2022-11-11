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

        LoadSettings();

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            GameExit();
        }
    }

    public void SaveDataReset()
    {
        StageManager.instance.ResetStageData();
        CurrencyManager.instance.ResetSaveData();
        UpgradeManager.instance.ResetUpgradeData();
        FleetManager.instance.ResetFleetData();
        FleetFormationManager.instance.ResetFomationData();

        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void GameExit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }


    /// �� �Ʒ��� <Setting>
    /// ���� ���������� ���Ǵ� value���� �ִ� ����
    public float BGMValue;
    public float SEValue;

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

    string _key_se_value   = "SEval";
    string _key_bgm_value  = "BGMval";
    string _key_diff_value = "DifficultyVal";
    // ����
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(_key_se_value, SEValue);
        PlayerPrefs.SetFloat(_key_bgm_value, BGMValue);
        PlayerPrefs.SetFloat(_key_diff_value, difficulty);
    }
    // ���� �ҷ�����
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey(_key_se_value))
        {
            SEValue = PlayerPrefs.GetFloat(_key_se_value);
        }
        if (PlayerPrefs.HasKey(_key_bgm_value))
        {
            BGMValue = PlayerPrefs.GetFloat(_key_bgm_value);
        }
        if (PlayerPrefs.HasKey(_key_se_value))
        {
            Difficulty = PlayerPrefs.GetFloat(_key_diff_value);
        }

        Debug.Log(string.Format("SE : {0}, BGM : {1}, DIFF : {2}", SEValue, BGMValue, Difficulty));
    }
}
