using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public GameObject[] parts;
    public float[] scaleYs;
    public float upTime = 0.2f;
    public float gapTime = 0.05f;
    public float distanceToCamera = 15;

    private void Update()
    {
        if (transform.position.z - Camera.main.transform.position.z < distanceToCamera)
        {
            enabled = false;
            StartCoroutine(DoJob());
        }
    }

    private IEnumerator DoJob()
    {
        int i = 0;
        foreach (var part in parts)
        {
            iTween.ScaleTo(part, iTween.Hash("y", scaleYs[i], "time", upTime));
            yield return new WaitForSeconds(gapTime);
            i++;
        }
    }
}
