using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSound : MonoBehaviour
{
    List<AudioSource> weaponSounds = new List<AudioSource> ();
    List<AudioSource> subWeaponSounds = new List<AudioSource>();
    List<AudioSource> shieldHitSounds = new List<AudioSource>();

    public Transform soundTrf;
    public Transform subSoundTrf;
    public Transform shieldHitSoundTrf;

    private void Start()
    {
        for (int i = 0; i < soundTrf.childCount; i++)
        {
            weaponSounds.Add(soundTrf.GetChild(i).gameObject.GetComponent<AudioSource>());
            weaponSounds[i].volume *= GameManager.instance.soundValue;
        }
        if(subSoundTrf != null)
        {
            for (int i = 0; i < subSoundTrf.childCount; i++)
            {
                subWeaponSounds.Add(subSoundTrf.GetChild(i).gameObject.GetComponent<AudioSource>());
                subWeaponSounds[i].volume *= GameManager.instance.soundValue;
            }
        }
        for (int i = 0; i < shieldHitSoundTrf.childCount; i++)
        {
            shieldHitSounds.Add(shieldHitSoundTrf.GetChild(i).gameObject.GetComponent<AudioSource>());
            shieldHitSounds[i].volume *= GameManager.instance.soundValue;
        }
    }

    public void FireSoundPlay()
    {
        int count = Random.Range(0, weaponSounds.Count);
        weaponSounds[count].Play();
    }
    public void FireSubSoundPlay()
    {
        int count = Random.Range(0, subWeaponSounds.Count);
        subWeaponSounds[count].Play();
    }
    public void ShieldHitSoundPlay()
    {
        int count = Random.Range(0, shieldHitSounds.Count);
        shieldHitSounds[count].Play();
    }
}
