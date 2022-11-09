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
            bgmInitVols.Add(audioSources[i].volume);
        }
        BgmRandomPlay();

        InvokeRepeating("BgmStopCheck", 60, 5);
    }


    public Transform BgmTransform;
    List<AudioSource> audioSources = new List<AudioSource>();
    List<float> bgmInitVols = new List<float>();

    bool isPlay = false;
    int tempPlayIndex = 0;

    public void BgmRandomPlay()
    {
        int count = Random.Range(0, audioSources.Count);

        audioSources[count].Play();
        tempPlayIndex = count;
        isPlay = true;
    }

    void BgmStopCheck()
    {
        if (!audioSources[tempPlayIndex].isPlaying)
        {
            BgmRandomPlay();
        }
    }

    public void BgmSettingSet()
    {
        float bgmValue = GameManager.instance.bgmValue;
        for(int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].volume = bgmInitVols[i] * bgmValue;
        }
    }
}
