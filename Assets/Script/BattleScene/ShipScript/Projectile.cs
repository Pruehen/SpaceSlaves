using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(this.transform.forward * 10, ForceMode.Impulse);
    }

    float dmg;

    public void Init(float dmg)
    {
        this.dmg = dmg;
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
            Destroy(this.gameObject);
        }
    }
}
