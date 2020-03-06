using UnityEngine;
using System.Collections;

public class MoveOnLine2 : MonoBehaviour
{
    public Vector3 deltaA, deltaB;
    public float speed = 2f;
    public iTween.LoopType loopType = iTween.LoopType.pingPong;
    public iTween.EaseType easyType = iTween.EaseType.easeInOutSine;
    public float delay = 0;

    [Header("")]
    public float distanceToPlayer = -1;
    public float distanceToCamera = -1;

    private Vector3 pointA, pointB;
    private Rigidbody rb;
    private GameObject temp;

    private void Start()
    {
        pointA = transform.position + deltaA;
        pointB = transform.position + deltaB;

        rb = GetComponent<Rigidbody>();

        temp = new GameObject("_Temp");
        temp.transform.position = transform.position;
        temp.hideFlags = HideFlags.HideInHierarchy;
    }

    private void Update()
    {
        if (distanceToCamera != -1 && transform.position.z - Camera.main.transform.position.z < distanceToCamera ||
            distanceToPlayer != -1 && transform.position.z - PlayerController.instance.transform.position.z < distanceToPlayer)
        {
            OnStart();
        }
    }

    private void OnStart()
    {
        if (temp == null)
        {
            enabled = false;
            return;
        }

        if (!CUtils.EqualVector3(transform.position, pointA))
        {
            iTween.MoveTo(temp, iTween.Hash("position", pointA, "speed", speed, "easeType", easyType, "oncomplete", "OnMoveToPointComplete", "oncompletetarget", gameObject, "onupdatetarget", gameObject, "onupdate", "OnUpdate"));
        }
        else
        {
            OnMoveToPointComplete();
        }
        enabled = false;
    }

    private void OnMoveToPointComplete()
    {
        iTween.MoveTo(temp, iTween.Hash("position", pointB, "looptype", loopType, "speed", speed, "easeType", easyType, "onupdatetarget", gameObject, "onupdate", "OnUpdate", "delay", delay));
    }

    private void OnUpdate()
    {
        rb.MovePosition(temp.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Plane"))
        {
            Destroy(temp);
        }
    }
}
