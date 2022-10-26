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

    public void TurretDataInit(float dmg, dmg_Type dmgType, float fireDelay)
    {
        this.dmg = dmg;
        this.dmgType = dmgType;
        this.fireDelay = fireDelay;

        Debug.Log(dmg + " " + dmgType + " " + fireDelay);
    }

    private void Start()
    {
        mainShipControl = this.transform.parent.parent.GetComponent<ShipControl>();
        laser = this.GetComponent<LineRenderer>();
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
            transform.LookAt(mainShipControl.target.transform.position);
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

        laserWidth *= 0.8f;
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
            Projectile projectile = Instantiate(Shell, this.transform.position, this.transform.rotation).GetComponent<Projectile>();
            projectile.Init(dmg);
        }
        mainShipControl.shipSound.FireSoundPlay();

        delayCount = 0;
        randomDelay = Random.Range(-fireDelay * 0.2f, fireDelay * 0.2f);
    }
}
