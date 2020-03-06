using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUpAndDown : MonoBehaviour
{
    public float goupValue = 3;
    public float goupTime = 0.5f, goDownTime = 0.3f;
    public float distanceToPlayer = 12;
    public float distanceToPlayerToGoDown = 0.5f;
    public bool fallByGravity;
    private bool isDone;

    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Update()
    {
        if (transform.position.z - PlayerController.instance.transform.position.z < distanceToPlayerToGoDown)
        {
            if (fallByGravity)
            {
                GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                iTween.MoveBy(gameObject, iTween.Hash("y", -goupValue, "time", goDownTime));
            }
            enabled = false;
            return;
        }

        if (!isDone && transform.position.z - PlayerController.instance.transform.position.z < distanceToPlayer)
        {
            isDone = true;
            iTween.MoveBy(gameObject, iTween.Hash("y", goupValue, "time", goupTime));
        }
    }
}
