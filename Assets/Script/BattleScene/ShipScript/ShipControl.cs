using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipControl : MonoBehaviour
{
    ShipAi shipAi;
    public List<Turret> turrets;
    public ShipSound shipSound;
    public ShipShield shipShield;

    Rigidbody rigidbody;
    LineRenderer laser;

    int id = -1;//�Լ� ���� ���̵�. 0���� ����
    public string shipName;//�Լ� �̸� (�Ա�)
    public ship_Class shipClass;//�Լ� ���� (����)
    public int cost;//�Լ� ���� ����
    public float dmg;//�ߴ� ���ݷ�, �ߴ� n�� ���� ������
    public dmg_Type dmgType;//���� Ÿ��
    public bool isTurret;//�ͷ� ����
    public float fireDelay;//���� �ӵ�, n�ʿ� 1ȸ ����
    public float maxRange;//�ִ� ��Ÿ�
    public float minRange;//�ּ� ��Ÿ�
    public float fitRange;//���� ��Ÿ�. �Լ��� �� �Ÿ��� �ӹ������� �����
    public float hp;//ü��
    public float df;//����
    public float sd;//��ȣ��    
    public float defaultspeed;//�⺻ �̵� �ӵ�
    public float agility;//��ȸ �ӵ�   

    float delayCount = 0;
    float randomDelay = 0;
    public bool isRange { private set; get; }

    public Vector3 toTargetVec;
    public Vector3 toTargetVec_Local;
    public LinkedList<GameObject> FoundTarget = new LinkedList<GameObject>();

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        laser = this.GetComponent<LineRenderer>();
        shipAi = this.GetComponent<ShipAi>();
        shipSound = this.GetComponent<ShipSound>();
        shipShield = this.GetComponent<ShipShield>();

        if (id == -1)
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

        shipName = refData.shipName;//�Լ� �̸� (�Ա�)
        shipClass = refData.shipClass;//�Լ� ���� (����)
        cost = refData.cost;//�Լ� ���� ����
        dmg = refData.dmg;//�ߴ� ���ݷ�, �ߴ� n�� ���� ������
        dmgType = refData.dmgType;//���� Ÿ��
        isTurret = refData.turretType;
        fireDelay = refData.fireDelay;//���� �ӵ�, n�ʿ� 1ȸ ����
        maxRange = refData.maxRange;//�ִ� ��Ÿ�
        minRange = refData.minRange;//�ּ� ��Ÿ�
        fitRange = refData.fitRange;//���� ��Ÿ�. �Լ��� �� �Ÿ��� �ӹ������� �����
        hp = refData.hp;//ü��
        df = refData.df;//����
        sd = refData.sd;//��ȣ��    
        defaultspeed = refData.defaultspeed;//�⺻ �̵� �ӵ�
        agility = refData.agility;//��ȸ �ӵ�   
    }
    

    public GameObject target;//���� �Լ��� �����ϰ� �ִ� Ÿ��
    //public LinkedListNode<GameObject> targetNode;

    void FixedUpdate()
    {
        if(dmgType == dmg_Type.particle && !isTurret)//������ ���� ������ ���, ������ ����
        {
            LaserGrapic();
        }
        
        if(target != null)//Ÿ�ٺ��� ��������
        {
            toTargetVec = target.transform.position - this.transform.position;//Ÿ���� ���ϴ� ����
            toTargetVec_Local = this.transform.InverseTransformDirection(toTargetVec).normalized;//�� ������ ����ȭ
        }       
        RotateTarget(toTargetVec_Local.x);//�Լ� �ڵ� ȸ�� ����

        if (delayCount <= fireDelay + randomDelay)//���ݼӵ� ���� ����
        {
            delayCount += Time.deltaTime;
        }

        if (isRange && delayCount > fireDelay + randomDelay)//��Ÿ� �� �� �ڵ� ���� ����
        {
            Attack();
        }

        if(target != null && !target.activeSelf)
        {
            TargetDestroyed();
        }
    }

    float defaultLaserWidth = 0.01f;
    float laserWidth;


    public float Hit(float dmg)//�Լ� �ǰ� �Լ�
    {
        float inputDmg = dmg;
        
        if (sd > 0 && inputDmg <= sd)
        {
            sd -= inputDmg;
            shipShield.EffectOn();
        }
        else if(sd > 0 && inputDmg > sd)
        {
            sd -= inputDmg;
            inputDmg = -sd;
            shipShield.EffectOn();
        }

        if (sd <= 0)
        {
            hp = hp - (inputDmg - df);
            if (hp <= 0)
            {
                ShipDestroy();
            }
        }

        return hp;
    }

    public GameObject shipDebriPrf;
    void ShipDestroy()
    {
        BattleSceneManager.instance.GameEndCheck();
        this.transform.SetParent(BattleSceneManager.instance.DestroyedShip);

        GameObject debri = Instantiate(shipDebriPrf, this.transform.position, this.transform.rotation);
        debri.GetComponent<Rigidbody>().velocity = rigidbody.velocity;

        this.gameObject.SetActive(false);
    }

    public GameObject shell;
    public GameObject cannon;

    void Attack()//���� �Լ�
    {
        if (target == null || Mathf.Abs(toTargetVec_Local.x) > 0.1f)
            return;

        delayCount = 0;
        ShipControl targetSC = target.GetComponent<ShipControl>();

        if (!isTurret)//���� ������ ���
        {            
            if (dmgType == dmg_Type.particle)//������ ������ ���, ������ �׷��� ���� �� ���̷�Ʈ ���� ����
            {
                laserWidth = defaultLaserWidth;
                laser.SetPosition(1, new Vector3(0, 0, toTargetVec.magnitude));
                shipSound.FireSoundPlay();
                if (targetSC.Hit(dmg) <= 0)
                {
                    TargetDestroyed();
                }
            }
            else if(dmgType == dmg_Type.explosion)
            {
                Missile missile = Instantiate(shell, cannon.transform.position, cannon.transform.rotation).GetComponent<Missile>();
                missile.Init(dmg, target.transform);
                shipSound.FireSoundPlay();
            }
        }
        else if(isTurret)//�ͷ��� ���
        {
            for(int i = 0; i < turrets.Count; i++)
            {
                turrets[i].Attack(targetSC);
            }
        }

        randomDelay = Random.Range(-fireDelay * 0.2f, fireDelay * 0.2f);
    }

    public void TargetDestroyed()
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

        laserWidth *= 0.8f;
    }

    void RotateTarget(float rotateOrder)//Ÿ�� �������� ���� ��ȯ �Լ�
    {
        rigidbody.AddTorque(this.transform.up * agility * 0.001f * rotateOrder, ForceMode.Force);
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
        rigidbody.AddForce(this.transform.forward * defaultspeed * 0.01f, ForceMode.Force);
    }

    public void MoveBack()//���� ��� �Լ�
    {
        rigidbody.AddForce(-this.transform.forward * defaultspeed * 0.005f, ForceMode.Force);
    }
}
