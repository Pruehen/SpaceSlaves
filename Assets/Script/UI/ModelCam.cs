using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCam : MonoBehaviour
{
    public List<GameObject> model = new List<GameObject>();
    public GameObject Cam;
    public Transform modelfound;
    public GameObject CamTarget;

    private void Start()
    {
        ModelsSet();
    }

    void ModelsSet()
    {
        Cam.SetActive(false);

        for (int i = 0; i < modelfound.childCount; i++)
        {
            model.Add(modelfound.GetChild(i).gameObject);
        }
    }

    public void CamSet()
    {
        CamTarget.gameObject.SetActive(true);
        Cam.gameObject.SetActive(true);
    }

    public void CamSetOff()
    {
        CamTarget.gameObject.SetActive(false);
        Cam.gameObject.SetActive(false);
    }
}
