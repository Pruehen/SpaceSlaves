using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject Message;

    public void GoLaborScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LaborScene");
    }
    public void GoDefenceScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("DefenceScene");
    }

    public void GoUpgradeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("UpgradeScene");
    }

    public void ExitButton()
    {        
        Message.SendMessage("MessageQ","종료하시겠습니까?");
        //Application.Quit();
    }

}
