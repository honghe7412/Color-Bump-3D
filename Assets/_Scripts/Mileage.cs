using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Mileage : MonoBehaviour
{
    [SerializeField]
    private Text distanceTxt;

    private float distance=0;

    private float firstPosition;

    private int mileageInt;

    [SerializeField]
    private GameObject[] recoreObjs;//新纪录的时候展示的obj；
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < recoreObjs.Length; i++)
        {
            recoreObjs[i].SetActive(false);
        }

        if(GameMaster.instance.IsRevive)
        {
            distance=GameMaster.instance.Mileage;
        }
        
        distanceTxt.text=distance+"m";
        firstPosition=PlayerController.instance.transform.position.z;

        StartCoroutine(A());
    }

    IEnumerator A()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);

            mileageInt=(int)((PlayerController.instance.transform.position.z-firstPosition)+distance+1);
            distanceTxt.text=mileageInt.ToString()+"m";

            if(PlayerController.instance.Isdead)
            {
                GameMaster.instance.IsRevive=false;

                GameMaster.instance.Mileage=mileageInt;

                if(mileageInt>Prefs.OnRecord)
                {
                    GameController.instance.OnaddGold(2);
                    Prefs.OnRecord=mileageInt;

                    for (int i = 0; i < recoreObjs.Length; i++)
                    {
                        recoreObjs[i].SetActive(true);
                    }
                }
                
                StopCoroutine(A());
                break;
            }
        }
    }
}
