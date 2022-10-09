using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipControl : MonoBehaviour
{
    ShipAi shipAi;

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

        shipAi.TargetFound();
    }

    public GameObject target;//현재 함선이 지시하고 있는 타겟
    //public LinkedListNode<GameObject> targetNode;

    void FixedUpdate()
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

        if (isRange && delayCount > fireDelay && Mathf.Abs(toTargetVec_Local.x) < 0.01f)//사거리 내 적 자동 공격 제어
        {
            delayCount = 0;
            Attack();
        }

        if(!target.activeSelf && target != null)
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

    void Attack()//공격 함수
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

    void LaserGrapic()//레이저 길이 그려주는 함수. 임시
    {
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;

        laserWidth *= 0.95f;
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


    public void MoveFor()//전진 명령 함수
    {
        rigidbody.AddForce(this.transform.forward * speed * 0.001f, ForceMode.Force);
    }

    public void MoveBack()//후진 명령 함수
    {
        rigidbody.AddForce(-this.transform.forward * speed * 0.001f, ForceMode.Force);
    }
}
