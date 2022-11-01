using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroBuilder : MonoBehaviour
{
    public List<GameObject> planetSystems;
    public Light sunLight;

    // Start is called before the first frame update
    void Start()
    {

        planetSystems[StageManager.instance.selectedStage].SetActive(true);


        switch (StageManager.instance.selectedStage)
        {
            case 0:
                sunLight.intensity = 1.7f;
                break;
            case 1:
                sunLight.intensity = 1.8f;
                break;
            case 2:
                sunLight.intensity = 2f;
                break;
            case 3:
                sunLight.intensity = 1.5f;
                break;
            case 4:
                sunLight.intensity = 1.2f;
                break;
            case 5:
                sunLight.intensity = 1f;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
