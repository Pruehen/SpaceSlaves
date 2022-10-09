using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager instance;

    private void Awake()
    {
        instance = this;
    }


    public Transform EnemyManager;
    public Transform FriendlyManager;
}
