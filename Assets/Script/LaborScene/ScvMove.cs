using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScvMove : MonoBehaviour
{
    [SerializeField] Transform baseTrf, mineralTrf;

    Vector3 target;
    bool isMindir = false;//���� �̳׶��� ���ϰ� �ִ°�?

    public GameObject mineral_Dumy;//�ڱⰡ ��� �ִ� ��������

    // Start is called before the first frame update
    void Start()
    {
        DirSet();
    }

    float moveSpeed = 3;
    int minAmount = 5;

    // Update is called once per frame
    void Update()
    {
        float totalMoveSpeed = moveSpeed + UpgradeManager.instance.GetTotalActiveVal(UPGRADE_TYPE.SCV_SPEED_UP);
        this.transform.position += this.transform.forward * totalMoveSpeed * Time.deltaTime;

        if ((target - this.transform.position).magnitude < 1f)
        {
            isMindir = !isMindir;
            DirSet();
        }
    }
    void DirSet()
    {
        if (isMindir)
        {
            target = mineralTrf.position;
            mineral_Dumy.SetActive(false);

            MinGet();
        }
        else
        {
            target = baseTrf.position;
            mineral_Dumy.SetActive(true);
        }
        this.transform.forward = target - this.transform.position;
    }

    void MinGet()//���� ���� �Լ�
    {
        int totalMineral = minAmount + (int)UpgradeManager.instance.GetTotalActiveVal(UPGRADE_TYPE.SCV_AMOUNT_UP);
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Mineral, totalMineral);
    }
}