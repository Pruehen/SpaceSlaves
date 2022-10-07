using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipControl : MonoBehaviour
{
    State ShipState;

    Rigidbody rigidbody;
    LineRenderer laser;

    public float dmg = 10;//발당 공격력
    public float fireDelay = 1;//공격 속도
    public float maxRange = 10;//최대 사거리
    public float minRange = 5;//최소 사거리
    public float fitRange = 5.5f;
    public float hp = 100;//체력
    public float df = 1;//방어력
    public float sd = 100;//보호막
    public float speed = 10;//이동 속도
    public float defaultspeed = 10;//기본 이동 속도
    public float agility = 10;//선회 속도
    public bool isDamage = false;

    float delayCount = 0;
    public bool isRange { private set; get; }

    public Vector3 toTargetVec;
    public Vector3 toTargetVec_Local;
    public List<GameObject> FoundTarget = new List<GameObject>();

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        laser = this.GetComponent<LineRenderer>();

        InvokeRepeating("RangeCheck", 0, 1);
    }

    public GameObject target;//현재 함선이 지시하고 있는 타겟

    void Update()
    {
        LaserGrapic();
        if(target != null)//타겟벡터 생성제어
        {
            toTargetVec = target.transform.position - this.transform.position;//타겟을 향하는 벡터
            toTargetVec_Local = this.transform.InverseTransformDirection(toTargetVec).normalized;//위 벡터의 로컬화
        }       
        RotateTarget(toTargetVec_Local.x);//함선 자동 회전 제어
        if (delayCount <= fireDelay)//공격속도 변수 제어
        {
            delayCount += Time.deltaTime;
        }


        if (isRange && delayCount > fireDelay && toTargetVec_Local.x < 0.1f)//사거리 내 적 자동 공격 제어
        {
            delayCount = 0;
            Attack();
        }

        StateControl();
    }


    float defaultLaserWidth = 0.01f;
    float laserWidth;

    void StateControl()
    {
        if (ShipState == State.run)
        {
            speed = defaultspeed;
            MoveFor();
        }
        else if (ShipState == State.Idle)
        {
            speed = 0;
        }
        else if (ShipState == State.Back)
        {
            speed = defaultspeed;
            MoveBack();
        }
    }

    void PlayerHP()
    {
        if (isDamage == true)
        {
            ShipControl call = target.GetComponent<ShipControl>();
            if (call.sd > 0)
            {
                call.sd -= dmg;
                Debug.Log(call.sd);
                if (call.sd < dmg)
                {
                    call.hp = call.hp + call.sd + call.df;
                }
            }
            else if (call.sd == 0)
            {
                call.hp = call.hp - (call.dmg - call.df);
                Debug.Log(call.hp);
            }

            if (call.hp <= 0)
            {
                target.SetActive(false);
                FoundTarget.Remove(target);
                target = null;
            }
        }
    }

    void Attack()//공격 함수
    {
        if (target == null)
            return;

        laserWidth = defaultLaserWidth;
        laser.SetPosition(1, new Vector3(0, 0, toTargetVec.magnitude));
        isDamage = true;
        PlayerHP();
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
