using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDebri : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10);
    }
}
