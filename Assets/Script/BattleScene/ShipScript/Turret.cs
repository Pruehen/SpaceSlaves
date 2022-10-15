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
    float agility;//(포탑의) 선회 속도

    public GameObject Shell;

    void TurretDataSet()
    {
        dmg = mainShipControl.dmg;
        dmgType = mainShipControl.dmgType;
        fireDelay = mainShipControl.fireDelay;
        agility = mainShipControl.agility;

        mainShipControl.turrets.Add(this);
    }

    private void Start()
    {
        mainShipControl = this.transform.parent.parent.GetComponent<ShipControl>();

        TurretDataSet();
    }

    private void Update()
    {
        if (mainShipControl.target != null)
        {
            transform.LookAt(mainShipControl.target.transform.position);
        }
    }

    public void Attack(ShipControl target)
    {
        if (dmgType == dmg_Type.particle)
        {
            if (target.Hit(dmg) <= 0)
            {
                mainShipControl.TargetDestroyed();
            }
        }
        else if(dmgType == dmg_Type.kinetic)
        {
            Projectile projectile = Instantiate(Shell, this.transform.position, this.transform.rotation).GetComponent<Projectile>();
            projectile.Init(dmg);
            mainShipControl.shipSound.FireSoundPlay();
        }
    }
}
