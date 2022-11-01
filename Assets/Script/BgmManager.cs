using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public static BgmManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < BgmTransform.childCount; i++)
        {
            audioSources.Add(BgmTransform.GetChild(i).gameObject.GetComponent<AudioSource>());
        }
        BgmRandomPlay();
    }

    public Transform BgmTransform;
    List<AudioSource> audioSources = new List<AudioSource>();

    int tempPlayIndex = -1;

    public void BgmRandomPlay()
    {
        audioSources[0].Play();
    }
}
