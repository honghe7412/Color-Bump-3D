using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTorque : MonoBehaviour 
{
    public float distanceToPlayer = 7;
    public Vector3 torque;

    private void Start()
    {
        GetComponent<Rigidbody>().mass = 0.01f;
    }

    private void Update()
    {
        if (transform.position.z - PlayerController.instance.transform.position.z < distanceToPlayer)
        {
            enabled = false;
            GetComponent<Rigidbody>().AddTorque(torque);
        }
    }
}
