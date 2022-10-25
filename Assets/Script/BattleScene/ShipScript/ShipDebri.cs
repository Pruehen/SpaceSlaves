using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDebri : MonoBehaviour
{
    float addTorquePower = 0.3f;

    List<AudioSource> weaponSounds = new List<AudioSource>();
    public Transform soundTrf;

    private void Start()
    {
        Destroy(gameObject, 7);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        if (Random.Range(0, 1f) > 0.5f)
        {
            rb.AddTorque(this.transform.forward * addTorquePower, ForceMode.Impulse);
        }
        if (Random.Range(0, 1f) > 0.5f)
        {
            rb.AddTorque(this.transform.up * addTorquePower, ForceMode.Impulse);
        }
        if (Random.Range(0, 1f) > 0.5f)
        {
            rb.AddTorque(this.transform.right * addTorquePower, ForceMode.Impulse);
        }


        DestroySoundPlay();
    }



    public void DestroySoundPlay()
    {
        for (int i = 0; i < soundTrf.childCount; i++)
        {
            weaponSounds.Add(soundTrf.GetChild(i).gameObject.GetComponent<AudioSource>());
        }
        int count = Random.Range(0, weaponSounds.Count);
        weaponSounds[count].Play();
    }
}

