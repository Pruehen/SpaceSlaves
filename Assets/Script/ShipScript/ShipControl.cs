using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipControl : MonoBehaviour
{
    Rigidbody rigidbody;
    LineRenderer laser;

    float dmg = 10;//�ߴ� ���ݷ�
    float fireDelay = 1;//���� �ӵ�
    float maxRange = 10;//�ִ� ��Ÿ�
    float minRange = 5;//�ּ� ��Ÿ�
    float fitRange = 7;
    float hp = 100;//ü��
    float df = 1;//����
    float sd = 100;//��ȣ��
    float speed = 10;//�̵� �ӵ�
    float agility = 10;//��ȸ �ӵ�

    float delayCount = 0;
    bool isRange = false;
    Vector3 toTargetVec;
    public List<GameObject> FoundTarget = new List<GameObject>();

    public GameObject Targets;

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        laser = this.GetComponent<LineRenderer>();

        InvokeRepeating("TargetFound", 1, 1);
    }

    private GameObject target;

    void Update()
    {
        LaserGrapic();
        toTargetVec = target.transform.position - this.transform.position;//Ÿ���� ���ϴ� ����
        Vector3 toTargetVec_Local = this.transform.InverseTransformDirection(toTargetVec).normalized;//�� ������ ����ȭ

        RotateTarget(toTargetVec_Local.x);

        if(delayCount <= fireDelay)
        {
            delayCount += Time.deltaTime;
        }

        if (isRange && delayCount > fireDelay)
        {
            delayCount = 0;
            Attack();
        }

        if (toTargetVec.magnitude * 10 > fitRange)
        {
            MoveFor();

        }
        else if (toTargetVec.magnitude * 10 <= fitRange)
        {
            MoveBack();
        }
    }

    void TargetFound()
    {
        float ShortDis;

        for (int i = 0; i < Targets.transform.childCount; i++)
        {
            FoundTarget.Add(Targets.transform.GetChild(i).gameObject);
        }

        ShortDis = Vector3.Distance(gameObject.transform.position, FoundTarget[0].transform.position);

        target = FoundTarget[0];

        foreach (GameObject found in FoundTarget)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < ShortDis)
            {
                target = found;
                ShortDis = Distance;
            }
        }

        Debug.Log(target);

        RangeCheck();
    }

    float defaultLaserWidth = 0.01f;
    float laserWidth;

    void Attack()//���� �Լ�
    {
        laserWidth = defaultLaserWidth;
        laser.SetPosition(1, new Vector3(0, 0, toTargetVec.magnitude));
    }

    void LaserGrapic()//������ ���� �׷��ִ� �Լ�. �ӽ�
    {
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;

        laserWidth *= 0.95f;
    }

    void MoveFor()//���� ��� �Լ�
    {
        rigidbody.AddForce(this.transform.forward * speed * 0.001f, ForceMode.Force);
    }

    void MoveBack()//���� ��� �Լ�
    {
        rigidbody.AddForce(-this.transform.forward * speed * 0.001f, ForceMode.Force);
    }

    void RotateTarget(float rotateOrder)//Ÿ�� �������� ���� ��ȯ �Լ�
    {
        rigidbody.AddTorque(this.transform.up * agility * 0.0001f * rotateOrder, ForceMode.Force);
    }

    void RangeCheck()//Ÿ�ٰ��� �Ÿ� üũ �Լ�. invoke�� �ʴ� 1ȸ�� ȣ��
    {
        if(target == null)
        {
            return;
        }

        Debug.Log(toTargetVec.magnitude * 10);

        if (toTargetVec.magnitude * 10 < maxRange && toTargetVec.magnitude * 10 > minRange)
        {
            isRange = true;
        }
        else
        {
            isRange = false;
        }
    }
}
