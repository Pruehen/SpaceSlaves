using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSound : MonoBehaviour
{
    List<AudioSource> weaponSounds = new List<AudioSource> ();
    List<AudioSource> subWeaponSounds = new List<AudioSource>();

    public Transform soundTrf;
    public Transform subSoundTrf;

    private void Start()
    {
        for (int i = 0; i < soundTrf.childCount; i++)
        {
            weaponSounds.Add(soundTrf.GetChild(i).gameObject.GetComponent<AudioSource>());
        }
        if(subSoundTrf != null)
        {
            for (int i = 0; i < subSoundTrf.childCount; i++)
            {
                subWeaponSounds.Add(subSoundTrf.GetChild(i).gameObject.GetComponent<AudioSource>());
            }
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
}
