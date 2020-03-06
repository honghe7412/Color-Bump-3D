using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInfinity : MonoBehaviour {

    public float speed = 100;
    private Vector3 m_EulerAngleVelocity;
    private Rigidbody rb;

    private bool isEnabled = true;

    private void Start()
    {
        m_EulerAngleVelocity = new Vector3(0, speed, 0);
        rb = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        if (!isEnabled) return;

        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Plane"))
        {
            isEnabled = false;
        }
    }
}
