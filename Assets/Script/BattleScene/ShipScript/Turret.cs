using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    ShipControl mainShipControl;

    float dmg;//�ߴ� ���ݷ�, �ߴ� n�� ���� ������
    dmg_Type dmgType;//���� Ÿ��
    float fireDelay;//���� �ӵ�, n�ʿ� 1ȸ ����
    float delayCount = 0;
    float randomDelay= 0;

    public GameObject Shell;
    Missile controledMissile;

    public void TurretDataInit(float dmg, dmg_Type dmgType, float fireDelay)
    {
        this.dmg = dmg;
        this.dmgType = dmgType;
        this.fireDelay = fireDelay;

        //Debug.Log(dmg + " " + dmgType + " " + fireDelay);
    }

    private void Start()
    {
        mainShipControl = this.transform.parent.parent.GetComponent<ShipControl>();
        laser = this.GetComponent<LineRenderer>();
        controledMissile = Shell.GetComponent<Missile>();

        randomDelay = Random.Range(-fireDelay * 0.2f, fireDelay * 0.2f);
        delayCount = fireDelay - fireDelay * 0.5f;
    }

    private void Update()
    {
        if (delayCount <= fireDelay + randomDelay)//�ͷ� ���ݼӵ� ���� ����
        {
            delayCount += Time.deltaTime;
        }

        if (dmgType == dmg_Type.particle)//������ �ͷ��� ���, ������ ����
        {
            LaserGrapic();
        }

        if (mainShipControl.target != null)
        {
            if (dmgType != dmg_Type.explosion)
            {
                transform.LookAt(mainShipControl.target.transform.position);//�̻��� �߻�Ⱑ �ƴ� ���, �ͷ����� �ڵ�����������
            }
            if(mainShipControl.isRange && delayCount >= fireDelay + randomDelay)
            {
                Attack(mainShipControl.target.GetComponent<ShipControl>());
            }
        }
    }
    LineRenderer laser;
    float laserWidth = 0;
    public float defaultLaserWidth = 0.01f;

    void LaserGrapic()//������ ���� �׷��ִ� �Լ�. �ӽ�
    {
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;

        laserWidth *= 0.9f;
    }

    void Attack(ShipControl target)
    {
        if (dmgType == dmg_Type.particle)
        {
            laserWidth = defaultLaserWidth;
            laser.SetPosition(1, new Vector3(0, 0, mainShipControl.toTargetVec.magnitude));
            if (target.Hit(dmg, 1.5f, 0.75f) <= 0)
            {
                mainShipControl.TargetDestroyed();
            }
        }
        else if(dmgType == dmg_Type.kinetic)
        {
            Projectile projectile = Shell.GetComponent<Projectile>();
            projectile.Init(dmg, this.transform.position, this.transform.rotation);
        }
        else if (dmgType == dmg_Type.explosion)
        {
            controledMissile.Init(dmg, target.transform, this.transform.position, this.transform.rotation);
        }
        mainShipControl.shipSound.FireSoundPlay();

        delayCount = 0;
        randomDelay = Random.Range(-fireDelay * 0.2f, fireDelay * 0.2f);
    }

    public void NewTargetSet(Transform target)
    {
        controledMissile.NewTargetSet(target);
    }
}
