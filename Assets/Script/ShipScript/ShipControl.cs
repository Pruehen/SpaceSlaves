using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        LaserGrapic();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack(target.transform.position);
        }

        RotateTarget(target.transform.position);
    }


    float defaultLaserWidth = 0.01f;
    float laserWidth;
    void Attack(Vector3 targetPos)
    {
        laserWidth = defaultLaserWidth;
        laser.SetPosition(1, new Vector3(0, 0, (target.transform.position - this.transform.position).magnitude));
    }

    void LaserGrapic()
    {
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;

        laserWidth *= 0.95f;
    }

    void MoveFor()
    {
        rigidbody.AddForce(this.transform.forward * speed * 0.01f, ForceMode.Force);
    }

    void RotateTarget(Vector3 target)
    {
        Vector3 toTargetVec = target - this.transform.position;
        toTargetVec = this.transform.InverseTransformDirection(toTargetVec);

        rigidbody.AddTorque(this.transform.up * agility * 0.001f * toTargetVec.x, ForceMode.Force);
    }
}
