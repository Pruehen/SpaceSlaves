using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    float dmg;

    public void Init(float dmg, Vector3 pos, Quaternion rot)
    {
        this.gameObject.SetActive(true);

        this.dmg = dmg;
        this.transform.position = pos;
        this.transform.rotation = rot;

        GetComponent<Rigidbody>().AddForce(this.transform.forward * 15, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            other.GetComponent<ShipControl>().Hit(dmg, 0.75f, 1.5f);

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.SetActive(false);
        }
    }
}
