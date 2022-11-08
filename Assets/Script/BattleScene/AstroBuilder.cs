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
        for (int i = 0; i < this.transform.childCount; i++)
        {
            planetSystems.Add(this.transform.GetChild(i).gameObject);
        }

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
            default:
                sunLight.intensity = 1 - (StageManager.instance.selectedStage - 5) * 0.05f;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
