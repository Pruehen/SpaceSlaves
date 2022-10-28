using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMove : MonoBehaviour
{
    [SerializeField] Transform bottom, top;

    Vector3 target;
    bool istop = false;

    public static LightMove instance;
    // Start is called before the first frame update
    void Start()
    {
        SetToTop(); 
    }

    float speed = 8f; 

    // Update is called once per frame
    void Update()
    {
        float upgraeAddVal = UpgradeManager.GetTotalActiveVal(UPGRADE_TYPE.SCV_SPEED_UP);

        float totalMoveSpeed = speed + upgraeAddVal;
        this.transform.position += this.transform.forward * totalMoveSpeed * Time.deltaTime;

        if ((target - this.transform.position).magnitude < 1f)
        {
            istop = !istop;
            SetToTop();
        }
    }

    void SetToTop()
    {
        if(istop)
        {
            target = top.position;
        }
        else
        {
            transform.position = bottom.position; 
        }
        this.transform.forward = target - this.transform.position;
    }
}
