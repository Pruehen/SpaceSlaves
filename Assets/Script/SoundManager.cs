using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
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
    private void Start()
    {
        clickSoundVol_Init = clickSound.volume;
        closeSoundVol_Init = closeSound.volume;
        shipBuildSoundVol_Init = shipBuildSound.volume;
    }

    public AudioSource clickSound, closeSound, shipBuildSound;
    float clickSoundVol_Init, closeSoundVol_Init, shipBuildSoundVol_Init;    

    public void SoundSettingSet()
    {
        float soundValue = GameManager.instance.soundValue;
        clickSound.volume = clickSoundVol_Init * soundValue;
        closeSound.volume = closeSoundVol_Init * soundValue;
        shipBuildSoundVol_Init = shipBuildSoundVol_Init * soundValue;
    }

    public void clickSoundOn()
    {
        clickSound.Play();
    }
    public void CloseSoundOn()
    {
        closeSound.Play();
    }
    public void ShipBuildSoundOn()
    {
        shipBuildSound.Play();
    }
}
