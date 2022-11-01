using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public GameObject SpinOj;

    public float rotateSpeed = 1f;

    private void Update()
    {
        transform.RotateAround(SpinOj.transform.position, Vector3.back , rotateSpeed);
    }
}
