using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    public void ToLaborScene()
    {
        SceneManager.LoadScene("LaborScene");
    }
}
