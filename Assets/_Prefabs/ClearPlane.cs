using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlane : MonoBehaviour
{
    private bool isClear;

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("Player"))
        {
           isClear=false;
           StopCoroutine(a());
        }
    }
   private void OnCollisionExit(Collision other)
   {
       if(other.gameObject.CompareTag("Player"))
       {   
           isClear=true;
           //StartCoroutine(a());
       }
   }

   IEnumerator a()
   {
       yield return new WaitForSeconds(20.0f);

        if(isClear)
        {
            Destroy(this.transform.parent.gameObject);
        }
   }
}
