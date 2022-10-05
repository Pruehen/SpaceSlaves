using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipControl : MonoBehaviour
{
    Rigidbody rigidbody;
    LineRenderer laser;

    float dmg = 10;//발당 공격력
    float fireDelay = 1;//공격 속도
    float maxRange = 10;//최대 사거리
    float minRange = 5;//최소 사거리
    float fitRange = 7;
    float hp = 100;//체력
    float df = 1;//방어력
    float sd = 100;//보호막
    float speed = 10;//이동 속도
    float agility = 10;//선회 속도

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
        toTargetVec = target.transform.position - this.transform.position;//타겟을 향하는 벡터
        Vector3 toTargetVec_Local = this.transform.InverseTransformDirection(toTargetVec).normalized;//위 벡터의 로컬화

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

    void Attack()//공격 함수
    {
        laserWidth = defaultLaserWidth;
        laser.SetPosition(1, new Vector3(0, 0, toTargetVec.magnitude));
    }

    void LaserGrapic()//레이저 길이 그려주는 함수. 임시
    {
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;

        laserWidth *= 0.95f;
    }

    void MoveFor()//전진 명령 함수
    {
        rigidbody.AddForce(this.transform.forward * speed * 0.001f, ForceMode.Force);
    }

    void MoveBack()//후진 명령 함수
    {
        rigidbody.AddForce(-this.transform.forward * speed * 0.001f, ForceMode.Force);
    }

    void RotateTarget(float rotateOrder)//타겟 방향으로 방향 전환 함수
    {
        rigidbody.AddTorque(this.transform.up * agility * 0.0001f * rotateOrder, ForceMode.Force);
    }

    void RangeCheck()//타겟과의 거리 체크 함수. invoke로 초당 1회씩 호출
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
