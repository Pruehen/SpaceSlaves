using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    float dmg;
    Transform target;
    public void Init(float dmg, Transform target)
    {
        this.dmg = dmg;
        this.target = target;
    }
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();

        Destroy(this.gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(this.transform.forward, ForceMode.Force);
        
        this.transform.Rotate(Vector3.Cross(this.transform.forward, (target.position - this.transform.position).normalized));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            other.GetComponent<ShipControl>().Hit(dmg);
            Destroy(this.gameObject);
        }
    }
}
