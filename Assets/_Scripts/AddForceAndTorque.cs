﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceAndTorque : MonoBehaviour
{

    public float distanceToPlayer = 7;
    public Vector3 force;
    public Vector3 torque;

    private void Start()
    {
        GetComponent<Rigidbody>().mass = 0.01f;
        GetComponent<Rigidbody>().drag = 0.5f;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Update()
    {
        if (transform.position.z - PlayerController.instance.transform.position.z < distanceToPlayer)
        {
            enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(force);
            GetComponent<Rigidbody>().AddTorque(torque);
        }
    }
}