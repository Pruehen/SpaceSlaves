using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    Rigidbody rigidbody;
    LineRenderer laser;

    float dmg = 10;
    float hp = 200;
    float speed = 10;
    float agility = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        laser = this.GetComponent<LineRenderer>();
    }

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        LaserGrapic();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        RotateTarget(target.position);
    }


    float defaultLaserWidth = 0.01f;
    float laserWidth;
    void Attack()
    {
        laserWidth = defaultLaserWidth;
    }

    void LaserGrapic()
    {
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;

        laserWidth *= 0.95f;
    }

    void MoveFor()
    {
        rigidbody.AddForce(this.transform.forward * speed * 0.001f, ForceMode.Force);
    }

    void RotateTarget(Vector3 target)
    {
        Vector3 toTargetVec = target - this.transform.position;
        toTargetVec = this.transform.InverseTransformDirection(toTargetVec);

        rigidbody.AddTorque(this.transform.up * agility * 0.001f * toTargetVec.x, ForceMode.Force);
    }
}
