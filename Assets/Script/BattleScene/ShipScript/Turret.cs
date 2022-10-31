using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    ShipControl mainShipControl;

    float dmg;//발당 공격력, 발당 n의 기초 데미지
    dmg_Type dmgType;//무기 타입
    float fireDelay;//공격 속도, n초에 1회 공격
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
        if (delayCount <= fireDelay + randomDelay)//터렛 공격속도 변수 제어
        {
            delayCount += Time.deltaTime;
        }

        if (dmgType == dmg_Type.particle)//레이저 터렛일 경우, 레이저 셋팅
        {
            LaserGrapic();
        }

        if (mainShipControl.target != null)
        {
            if (dmgType != dmg_Type.explosion)
            {
                transform.LookAt(mainShipControl.target.transform.position);//미사일 발사기가 아닐 경우, 터렛방향 자동조정시켜줌
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

    void LaserGrapic()//레이저 길이 그려주는 함수. 임시
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
