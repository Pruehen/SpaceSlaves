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
    float agility;//(��ž��) ��ȸ �ӵ�

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
