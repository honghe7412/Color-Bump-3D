using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardbar : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Handheld.Vibrate();
            other.gameObject.GetComponent<MeshRenderer>().sharedMaterial=GetComponent<MeshRenderer>().sharedMaterial;
        }
    }
}
