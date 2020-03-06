using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MultyArray
{
    [SerializeField]
    private List<GameObject> objs;

    public GameObject this[int index]
    {
        get { return objs[index];}
    }

    public void RemveAt(int Int)
    {
        objs.RemoveAt(Int);
    }

    public int Length
    {
        get{return objs.Count;}
    }
}
