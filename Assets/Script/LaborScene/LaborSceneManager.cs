using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LaborSceneManager : MonoBehaviour
{
    public void ToBattleScene()
    {
        SceneManager.LoadScene("DefenceScene");
    }
}
