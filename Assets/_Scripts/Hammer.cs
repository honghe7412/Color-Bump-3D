using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour {
    public int direction = -1;
    public float time = 0.5f;
    public float waitTime = 0.5f;

    public float distanceToCamera = 20f;

    private void Update()
    {
        if (transform.position.z - Camera.main.transform.position.z < distanceToCamera)
        {
            enabled = false;
            StartCoroutine(Rotate());
        }
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            iTween.RotateBy(gameObject, iTween.Hash("y", 0.5f * direction, "time", time));
            Sound.instance.Play(Sound.Others.Rotate,0.1f);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
