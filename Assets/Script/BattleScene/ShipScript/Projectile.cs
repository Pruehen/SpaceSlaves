using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TrailRenderer = GetComponent<TrailRenderer>();
        this.gameObject.SetActive(false);
        this.transform.SetParent(null);
        pierceValue = pierceCount;
    }

    protected float dmg;
    TrailRenderer TrailRenderer;

    public void Init(float dmg, Vector3 pos, Quaternion rot)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.SetActive(true);
        
        this.dmg = dmg;
        this.transform.position = pos;
        this.transform.rotation = rot;

        TrailRenderer.Clear();

        pierceValue = pierceCount;
        GetComponent<Rigidbody>().AddForce(this.transform.forward * 10, ForceMode.Impulse);
        Debug.Log(this.transform.rotation);
    }

    public int pierceCount = 1;
    int pierceValue;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            other.GetComponent<ShipControl>().Hit(dmg, 0.75f, 1.5f);
            pierceValue--;

            if (pierceValue <= 0)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.gameObject.SetActive(false);
            }
        }
    }
}
