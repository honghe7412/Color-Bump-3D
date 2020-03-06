using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUp : MonoBehaviour
{
    public float goupValue;
    public float time = 0.2f;

    public float distanceToPlayer = 2;

    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Update()
    {
        if (transform.position.z - PlayerController.instance.transform.position.z < distanceToPlayer)
        {
            iTween.MoveBy(gameObject, iTween.Hash("y", goupValue, "time", time, "oncomplete", "OnComplete"));
            enabled = false;
        }
    }

    private void OnComplete()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
