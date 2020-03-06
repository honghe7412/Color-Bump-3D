using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Gold : MonoBehaviour
{
    [SerializeField]
    private Text goldNum;

    [SerializeField]
    private GameObject img;

    public static Gold Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance==null)
            Instance=this;
    }
    void Start()
    {
        //Prefs.GoldManage=3;
        goldNum.text=Prefs.GoldManage.ToString();
    }

    public void AddGold()
    {
        Prefs.GoldManage++;

        goldNum.text=Prefs.GoldManage.ToString();
    }

    public void SpendGold()
    {
        Prefs.GoldManage--;

        goldNum.text=Prefs.GoldManage.ToString();
    }

    public void PingPang()
    {
        img.transform.DOPunchScale(new Vector3(0.5f,0.5f,1f),0.5f);
    }
}
