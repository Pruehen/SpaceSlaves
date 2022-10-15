using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSound : MonoBehaviour
{
    List<AudioSource> weaponSounds = new List<AudioSource> ();
    public Transform soundTrf;
    private void Start()
    {
        for (int i = 0; i < soundTrf.childCount; i++)
        {
            weaponSounds.Add(soundTrf.GetChild(i).gameObject.GetComponent<AudioSource>());
        }
    }

    public void FireSoundPlay()
    {
        int count = Random.Range(0, weaponSounds.Count);
        weaponSounds[count].Play();
    }
}
