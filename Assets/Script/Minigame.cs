using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public GameObject pointOne;
    public GameObject poinTwo;

    public GameObject SpinOne;
    public GameObject SpinTwo;

    // the winner is...
    bool isGameOver = false;

    bool isSuccesseAble = false;
    bool isSpinActive = false;


    public void Shoot()
    {
        if (isGameOver || !isSpinActive)
            return;
        isGameOver = isSuccesseAble;

        StopSpinner();

        if (isSuccesseAble)
        {
            OnSuccesse();
        }
        else 
        { 
            Invoke("PlaySpinner", 1);
        }
    }

    void PlaySpinner()
    { 
        isSpinActive = true;
    }

    void StopSpinner()
    {
        isSpinActive = false;
    }

    void Reward()
    {
        Debug.Log("minigame winner ");
    }

    void OnSuccesse()
    {
        Invoke("Reward", 2.75f);
    }

    void GameStart()
    {
        isGameOver = false;
        isSpinActive = false;
        isSuccesseAble = false;

        Invoke("PlaySpinner", 3);
    }

    // Start is called before the first frame update
    void Start()
    {
        // init
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;
        float minDist = 50;
        var dist = Vector3.Distance(poinTwo.transform.position, pointOne.transform.position);
        isSuccesseAble = dist < minDist;

        float spinSpeed = 100;
        SpinOne.transform.Rotate(Vector3.back, spinSpeed * Time.deltaTime);
        SpinTwo.transform.Rotate(Vector3.back, -spinSpeed * Time.deltaTime );
    }
}
