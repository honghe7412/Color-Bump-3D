using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByLoop : MonoBehaviour {
    public float numRound;
    public float time;
    public iTween.LoopType loopType = iTween.LoopType.pingPong;
    public float delay;

    private void Start()
    {
        iTween.RotateBy(gameObject, iTween.Hash("y", numRound, "looptype", loopType, "time", time, "easeType", iTween.EaseType.easeInOutSine, "delay", delay));
    }
}
