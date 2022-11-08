using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScvMove : MonoBehaviour
{
    [SerializeField] Transform baseTrf, mineralTrf;

    Vector3 target;
    bool isMindir = false;//���� �̳׶��� ���ϰ� �ִ°�?

    public GameObject mineral_Dumy;//�ڱⰡ ��� �ִ� ��������

    public static ScvMove instance; 
    // Start is called before the first frame update
    void Start()
    {
        DirSet();
    }

    float moveSpeed = 5.4f;
    public int minAmount = 5;

    // Update is called once per frame
    void Update()
    {
        float upgraeAddVal = UpgradeManager.GetTotalActiveVal(UPGRADE_TYPE.SCV_SPEED_UP);

        float totalMoveSpeed = moveSpeed + upgraeAddVal;
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
        int totalMineral = minAmount + (int)UpgradeManager.GetTotalActiveVal(UPGRADE_TYPE.SCV_AMOUNT_UP);
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Mineral, totalMineral);
    }
}
