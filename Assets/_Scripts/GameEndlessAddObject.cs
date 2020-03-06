using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndlessAddObject : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objects=new List<GameObject>();
    private List<GameObject> objectsBackup=new List<GameObject>();
    private GameObject obj;
    private Vector3 position;
    private float maxSortZ=0.0f;
    private float minSortZ=0.0f;

    public MultyArray[] marray;
    private float lastZ;

    float firstPosition;

    [SerializeField]
    private float spacing=8.0f;
    // Start is called before the first frame update
    void Start()
    {
        MultyArray ma=marray[SelectLevel()];  //根据关卡登记选择障碍物组；

        while(ma.Length>0)
        {
            objects.Add(ma[0]);  //赋给所用的列表
            ma.RemveAt(0);
        }

        position=PlayerController.instance.transform.position;
        position+=new Vector3(0,0,10.8f);

        obj=Instantiate(objects[Random.Range(0,objects.Count)] ,position,Quaternion.identity);
        obj.transform.SetParent(this.transform.parent);

        AddClearObjectBehaviour();
    }

    int SelectLevel()
    {
        int Int=0;
        
        int levelInt=GameMaster.instance.CurrentLevelInt;
        
        Int=Mathf.CeilToInt(levelInt/(10));
        
        if(levelInt%10==0)
            Int--;

        return Int;
    }

    void AddClearObjectBehaviour()
    {
        obj.AddComponent<ClearObject>();
    }

    IEnumerator a()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    void OnInstantiate()
    {
        if(objects.Count<=0)
        {
            while(objectsBackup.Count>0)
            {
                objects.Add(objectsBackup[0]);
                objectsBackup.RemoveAt(0);
            }
        }
        
        if(obj)
            lastZ=obj.transform.position.z;  //记录上个物体的z值赋值给lastZ；

        maxSortZ=lastZ;                      //把lastZ赋值给maxSortZ初始化；

        OnMaxSortZ(obj);                     //记录obj中子物体的最大的Z值；

        float differenceMax=maxSortZ-lastZ;  //取到obj中子物体的最大的Z值和obj中心值得差值；
        
        int keyInt=Random.Range(0,objects.Count);

        obj=Instantiate(objects[keyInt]);

        objectsBackup.Add(objects[keyInt]);
        objects.RemoveAt(keyInt);

        obj.transform.SetParent(this.transform.parent);

        firstPosition=obj.transform.position.z; //记录这个物体的初始位置

        minSortZ=firstPosition;                 //初始化minSortZ
        
        OnMinSortZ(obj);                        //记录obj中子物体的最小的Z值

        float differenceMin=firstPosition-minSortZ; //取差值；
        
        if(obj)
        {
            obj.transform.position=new Vector3(obj.transform.position.x,obj.transform.position.y,lastZ+differenceMin+differenceMax+spacing);
        }

        AddClearObjectBehaviour();
    }

    public void OndestroyFirst()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(-PlayerController.instance.transform.position.z+obj.transform.position.z<5.5f)
        {
            OnInstantiate();
        }
    }

    void OnMaxSortZ(GameObject obj)
    {
        if(obj==null)
            return;

        foreach (Transform item in obj.transform)
        {
            if(item==null)
                continue;
            
            if(item.position.z>maxSortZ)
                maxSortZ=item.position.z;
            
            OnMaxSortZ(item.gameObject);
        }
    }

    void OnMinSortZ(GameObject obj)
    {
        if(obj==null)
            return;

        foreach (Transform item in obj.transform)
        {
            if(item==null)
                continue;
            
            if(item.position.z<minSortZ)
                minSortZ=item.position.z;

            OnMinSortZ(item.gameObject);
        }
    }
}
