using UnityEngine;
using System.Collections;

public class MoveOnLine3 : MonoBehaviour
{
    public float fromX, toX;

    public float speed = 0.7f;
    private Vector3 pointA, pointB;

    private void Start()
    {

        pointA = new Vector3(fromX, transform.position.y, transform.position.z);
        pointB = new Vector3(toX, transform.position.y, transform.position.z);

        if (!CUtils.EqualVector3(transform.position, pointA))
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", pointA, "speed", speed, "easeType", iTween.EaseType.linear, "oncomplete", "OnMoveToPointComplete"));
        }
        else
        {
            OnMoveToPointComplete();
        }
    }

    private void OnMoveToPointComplete()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", pointB, "looptype", "pingpong", "speed", speed, "easeType", iTween.EaseType.linear));
    }
}
