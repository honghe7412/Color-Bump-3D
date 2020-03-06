using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    public bool scaleX;
    public float scaleXValue;
    public bool scaleY;
    public float scaleYValue;
    public bool scaleZ;
    public float scaleZValue;

    public float time = 0.2f;

    public float distanceToCamera = -1f;
    public float distanceToPlayer = -1f;

    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Update()
    {
        if (distanceToCamera != -1 && transform.position.z - Camera.main.transform.transform.position.z < distanceToCamera ||
            distanceToPlayer != -1 && transform.position.z - PlayerController.instance.transform.position.z < distanceToPlayer)
        {
            var scale = transform.localScale;
            if (scaleX) scale.x = scaleXValue;
            if (scaleY) scale.y = scaleYValue;
            if (scaleZ) scale.z = scaleZValue;

            GetComponent<Collider>().enabled = false;
            iTween.ScaleTo(gameObject, iTween.Hash("scale", scale, "time", time, "oncomplete", "OnComplete"));
            enabled = false;
        }
    }
    
    private void OnComplete()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
