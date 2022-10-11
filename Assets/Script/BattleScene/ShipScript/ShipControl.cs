using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipControl : MonoBehaviour
{
    ShipAi shipAi;

    Rigidbody rigidbody;
    LineRenderer laser;

    int id = -1;//함선 고유 아이디. 0부터 시작
    public string shipName;//함선 이름 (함급)
    public ship_Class shipClass;//함선 종류 (함종)
    public int cost;//함선 생산 가격
    public float dmg;//발당 공격력, 발당 n의 기초 데미지
    public dmg_Type dmgType;//무기 타입
    public float fireDelay;//공격 속도, n초에 1회 공격
    public float maxRange;//최대 사거리
    public float minRange;//최소 사거리
    public float fitRange;//적정 사거리. 함선은 이 거리에 머무르려고 노력함
    public float hp;//체력
    public float df;//방어력
    public float sd;//보호막    
    public float defaultspeed;//기본 이동 속도
    public float agility;//선회 속도   

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

        if(id == -1)
        {
            idSet(0);
        }
        InvokeRepeating("RangeCheck", 0, 1);

        //shipAi.TargetFound();
    }    
    public void idSet(int id)
    {
        this.id = id;
        ShipInfoData refData = FleetManager.instance.GetShipData(id);

        shipName = refData.shipName;//함선 이름 (함급)
        shipClass = refData.shipClass;//함선 종류 (함종)
        cost = refData.cost;//함선 생산 가격
        dmg = refData.dmg;//발당 공격력, 발당 n의 기초 데미지
        dmgType = refData.dmgType;//무기 타입
        fireDelay = refData.fireDelay;//공격 속도, n초에 1회 공격
        maxRange = refData.maxRange;//최대 사거리
        minRange = refData.minRange;//최소 사거리
        fitRange = refData.fitRange;//적정 사거리. 함선은 이 거리에 머무르려고 노력함
        hp = refData.hp;//체력
        df = refData.df;//방어력
        sd = refData.sd;//보호막    
        defaultspeed = refData.defaultspeed;//기본 이동 속도
        agility = refData.agility;//선회 속도   
    }

    public GameObject target;//현재 함선이 지시하고 있는 타겟
    //public LinkedListNode<GameObject> targetNode;

    void FixedUpdate()
    {
        if(dmgType == dmg_Type.particle)
        {
            LaserGrapic();
        }
        
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

        if (isRange && delayCount > fireDelay && Mathf.Abs(toTargetVec_Local.x) < 0.1f)//사거리 내 적 자동 공격 제어
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
                BattleSceneManager.instance.GameEndCheck();
                this.transform.SetParent(BattleSceneManager.instance.DestroyedShip);
                this.gameObject.SetActive(false);         
            }
        }

        return hp;
    }

    void Attack()//공격 함수
    {
        if (target == null)
            return;

        ShipControl targetSC = target.GetComponent<ShipControl>();

        if (dmgType == dmg_Type.particle)
        {
            laserWidth = defaultLaserWidth;
            laser.SetPosition(1, new Vector3(0, 0, toTargetVec.magnitude));
        }

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

        laserWidth *= 0.8f;
    }

    void RotateTarget(float rotateOrder)//타겟 방향으로 방향 전환 함수
    {
        rigidbody.AddTorque(this.transform.up * agility * 0.001f * rotateOrder, ForceMode.Force);
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
        rigidbody.AddForce(this.transform.forward * defaultspeed * 0.01f, ForceMode.Force);
    }

    public void MoveBack()//후진 명령 함수
    {
        rigidbody.AddForce(-this.transform.forward * defaultspeed * 0.005f, ForceMode.Force);
    }
}
