using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipControl : MonoBehaviour
{
    ShipAi shipAi;

    Rigidbody rigidbody;
    LineRenderer laser;

    public float dmg = 10;//�ߴ� ���ݷ�
    public float fireDelay = 1;//���� �ӵ�
    public float maxRange = 10;//�ִ� ��Ÿ�
    public float minRange = 5;//�ּ� ��Ÿ�
    public float fitRange = 5.5f;
    public float hp = 100;//ü��
    public float df = 1;//����
    public float sd = 100;//��ȣ��
    public float speed = 10;//�̵� �ӵ�
    public float defaultspeed = 10;//�⺻ �̵� �ӵ�
    public float agility = 10;//��ȸ �ӵ�

    float delayCount = 0;
    public bool isRange { private set; get; }

    public Vector3 toTargetVec;
    public Vector3 toTargetVec_Local;
    public LinkedList<GameObject> FoundTarget = new LinkedList<GameObject>();

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        laser = this.GetComponent<LineRenderer>();
        shipAi = this.GetComponent<ShipAi>();

        InvokeRepeating("RangeCheck", 0, 1);

        //shipAi.TargetFound();
    }

    public GameObject target;//���� �Լ��� �����ϰ� �ִ� Ÿ��
    //public LinkedListNode<GameObject> targetNode;

    void FixedUpdate()
    {
        LaserGrapic();
        if(target != null)//Ÿ�ٺ��� ��������
        {
            toTargetVec = target.transform.position - this.transform.position;//Ÿ���� ���ϴ� ����
            toTargetVec_Local = this.transform.InverseTransformDirection(toTargetVec).normalized;//�� ������ ����ȭ
        }       
        RotateTarget(toTargetVec_Local.x);//�Լ� �ڵ� ȸ�� ����
        if (delayCount <= fireDelay)//���ݼӵ� ���� ����
        {
            delayCount += Time.deltaTime;
        }

        if (isRange && delayCount > fireDelay && Mathf.Abs(toTargetVec_Local.x) < 0.01f)//��Ÿ� �� �� �ڵ� ���� ����
        {
            delayCount = 0;
            Attack();
        }

        if(target != null && !target.activeSelf)
        {
            TargetDestroyed();
        }
    }


    float defaultLaserWidth = 0.01f;
    float laserWidth;


    public float Hit(float dmg)
    {
        float inputDmg = dmg;

        if (sd > 0 && inputDmg <= sd)
        {
            sd -= inputDmg;
        }
        else if(sd > 0 && inputDmg > sd)
        {
            sd -= inputDmg;
            inputDmg = -sd;
        }

        if (sd <= 0)
        {
            hp = hp - (inputDmg - df);
            if (hp <= 0)
            {
                this.gameObject.SetActive(false);         
            }
        }

        return hp;
    }

    void Attack()//���� �Լ�
    {
        ShipControl targetSC = target.GetComponent<ShipControl>();

        if (target == null)
            return;

        laserWidth = defaultLaserWidth;
        laser.SetPosition(1, new Vector3(0, 0, toTargetVec.magnitude));

        if(targetSC.Hit(dmg) <= 0)
        {
            TargetDestroyed();
        }
    }

    void TargetDestroyed()
    {
        FoundTarget.Remove(target);
        target = null;
        if (FoundTarget.Count > 0)
        {
            shipAi.TargetFound();
        }
    }

    void LaserGrapic()//������ ���� �׷��ִ� �Լ�. �ӽ�
    {
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;

        laserWidth *= 0.95f;
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

        //Debug.Log(toTargetVec.magnitude * 10);

        if (toTargetVec.magnitude * 10 < maxRange && toTargetVec.magnitude * 10 > minRange)
        {
            isRange = true;
        }
        else
        {
            isRange = false;
        }
    }


    public void MoveFor()//���� ��� �Լ�
    {
        rigidbody.AddForce(this.transform.forward * speed * 0.001f, ForceMode.Force);
    }

    public void MoveBack()//���� ��� �Լ�
    {
        rigidbody.AddForce(-this.transform.forward * speed * 0.001f, ForceMode.Force);
    }
}
