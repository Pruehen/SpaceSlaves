using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void GoLaborScene()
    {
        SceneManager.LoadScene("LaborScene");
    }
    public void GoDefenceScene()
    {
        SceneManager.LoadScene("DefenceScene");
    }

    public void GoUpgradeScene()
    {
        SceneManager.LoadScene("UpgradeScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
