using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    float dmg;
    Transform target;
    public void Init(float dmg, Transform target, Vector3 pos, Quaternion rot)
    {
        gameObject.SetActive(true);
        this.dmg = dmg;
        this.target = target;
        this.transform.position = pos;
        this.transform.rotation = rot;
    }
    public void NewTargetSet(Transform target)
    {
        this.target = target;
    }
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();

        ObjectReset();
    }

    public float speed = 1;
    public float agility = 10;
    public float lifeTime = 10;
    float lifeTimeCount = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject.activeSelf)
        {
            rigidbody.AddForce(this.transform.forward * speed * Time.timeScale, ForceMode.Force);

            //this.transform.Rotate(Vector3.Cross(this.transform.forward, (target.position - this.transform.position).normalized));
            rigidbody.AddTorque(Vector3.Cross(this.transform.forward, (target.position - this.transform.position).normalized) * agility, ForceMode.Force);
            //Guided();
            lifeTimeCount += Time.deltaTime;
            if(lifeTimeCount > lifeTime)
            {
                ObjectReset();
            }
        }
    }

    Vector3 angleError_diff;
    Vector3 angleError_temp;

    void Guided()//사용하지 않음
    {
        Vector3 toTargetVec = target.position - this.transform.position;//missile to target Vector
        toTargetVec = toTargetVec.normalized;
        angleError_diff = toTargetVec - angleError_temp;//미분항
        angleError_temp = toTargetVec;

        Vector3 orderVec = angleError_diff;

        Vector3 side1 = toTargetVec;
        Vector3 side2 = side1 + orderVec;
        Vector3 orderAxis = Vector3.Cross(side1, side2);


        //rigidbody.AddTorque(Vector3.ClampMagnitude(orderAxis * (rigidbody.velocity.magnitude * 0.002f), 10), ForceMode.Force);
        this.transform.Rotate(orderAxis * 500);
    }
    public GameObject Effect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            other.GetComponent<ShipControl>().Hit(dmg, 1, 1);
            /*GameObject effect = this.transform.GetChild(0).gameObject;
            effect.transform.SetParent(null);
            effect.SetActive(true);*/

            GameObject effect = Instantiate(Effect, this.transform.position, Quaternion.identity, null);

            Destroy(effect, 5);

            ObjectReset();
        }
    }

    void ObjectReset()
    {
        lifeTimeCount = 0;
        rigidbody.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
}
