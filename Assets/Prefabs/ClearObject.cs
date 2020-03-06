using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(a());
    }

    IEnumerator a()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);

            if(PlayerController.instance.transform.position.z-this.transform.position.z>15.0f)
            {
                Destroy(this.gameObject);
                StopCoroutine(a());
            }
        }   
    }
}
