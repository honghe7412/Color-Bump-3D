using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgs : MonoBehaviour
{
    [SerializeField]
    private Material[] bgs;
    
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshRenderer>().sharedMaterial=bgs[Random.Range(0,bgs.Length)];
    }
}
