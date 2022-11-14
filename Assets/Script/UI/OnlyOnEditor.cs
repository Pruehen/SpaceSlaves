using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyOnEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!Application.isEditor)
            return;

        var cnt = transform.childCount;
        for (int i = 0; i < cnt; i++)
        {
            var goChild = transform.GetChild(i);
            goChild.gameObject.SetActive(Application.isEditor);
        }
    }
}
