using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDebri : MonoBehaviour
{
    float addTorquePower = 0.3f;
    private void Start()
    {
        Destroy(gameObject, 60);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        if (Random.Range(0, 1f) > 0.5f)
        {
            rb.AddTorque(this.transform.forward * addTorquePower, ForceMode.Impulse);
        }
        if (Random.Range(0, 1f) > 0.5f)
        {
            rb.AddTorque(this.transform.up * addTorquePower, ForceMode.Impulse);
        }
        if (Random.Range(0, 1f) > 0.5f)
        {
            rb.AddTorque(this.transform.right * addTorquePower, ForceMode.Impulse);
        }
    }
}
