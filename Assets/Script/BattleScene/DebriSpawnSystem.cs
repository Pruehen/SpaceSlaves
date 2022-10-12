using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebriSpawnSystem : MonoBehaviour
{
    public List<GameObject> debriPrf = new List<GameObject>();

    public void DebriCreate(Vector3 position, Quaternion rotation, int id, Vector3 velocity)
    {
        GameObject debri = Instantiate(debriPrf[id], position, rotation);
        debri.GetComponent<Rigidbody>().velocity = velocity;
    }
}
