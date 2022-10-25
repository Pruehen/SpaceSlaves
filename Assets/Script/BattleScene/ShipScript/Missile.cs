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

        //this.transform.Rotate(Vector3.Cross(this.transform.forward, (target.position - this.transform.position).normalized));
        rigidbody.AddTorque(Vector3.Cross(this.transform.forward, (target.position - this.transform.position).normalized) * 10, ForceMode.Force);
        //Guided();
    }

    Vector3 angleError_diff;
    Vector3 angleError_temp;

    void Guided()
    {
        Vector3 toTargetVec = target.position - this.transform.position;//missile to target Vector
        toTargetVec = toTargetVec.normalized;
        angleError_diff = toTargetVec - angleError_temp;//¹ÌºÐÇ×
        angleError_temp = toTargetVec;

        Vector3 orderVec = angleError_diff;

        Vector3 side1 = toTargetVec;
        Vector3 side2 = side1 + orderVec;
        Vector3 orderAxis = Vector3.Cross(side1, side2);


        //rigidbody.AddTorque(Vector3.ClampMagnitude(orderAxis * (rigidbody.velocity.magnitude * 0.002f), 10), ForceMode.Force);
        this.transform.Rotate(orderAxis * 500);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            other.GetComponent<ShipControl>().Hit(dmg, 1, 1);
            GameObject effect = this.transform.GetChild(0).gameObject;
            effect.transform.SetParent(null);
            effect.SetActive(true);


            Destroy(effect, 5);
            Destroy(this.gameObject);
        }
    }
}
