using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlane : MonoBehaviour
{
    public GameObject jointPosition;

    public Vector3 GetPosition
    {
        get
        {
            return jointPosition.transform.position;
        }
    }
}
