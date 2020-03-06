using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChildrenOnLine : MonoBehaviour
{
    public Vector3 deltaA, deltaB;
    public float speed = 0.7f;
    public iTween.LoopType loopType = iTween.LoopType.pingPong;
    public iTween.EaseType easyType = iTween.EaseType.linear;

    public float distanceToPlayer;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            var moveOnLine = child.gameObject.AddComponent<MoveOnLine2>();
            moveOnLine.deltaA = deltaA;
            moveOnLine.deltaB = deltaB;
            moveOnLine.speed = speed;
            moveOnLine.loopType = loopType;
            moveOnLine.easyType = easyType;
            moveOnLine.distanceToPlayer = distanceToPlayer;
        }
    }
}
