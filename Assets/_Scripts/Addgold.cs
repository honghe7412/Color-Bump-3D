using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Addgold : MonoBehaviour
{
    private Image img;

    [SerializeField]
    private GameObject subtrahend;
    //private Color color;0
    // Start is called before the first frame update
    void Awake()
    {
        img=this.GetComponent<Image>();
        //subtrahend.SetActive(false);
    }
    void Start()
    {
        
    }
    public void OnAddGold()
    {
        subtrahend.GetComponent<Text>().text="+1";
        DOTween.To(()=>this.transform.position,x=>this.transform.position=x,Gold.Instance.transform.position,0.5f).OnComplete(()=>
        {
            Gold.Instance.AddGold();
            Gold.Instance.PingPang();

            Destroy(this.gameObject);

        }).SetEase(Ease.InBack);
    }

    public void OnSpendGold()
    {
        subtrahend.GetComponent<Text>().text="-1";
        this.transform.position=Gold.Instance.transform.position;
        Vector3 goldPosition=Gold.Instance.transform.position;

        Color color=img.color;
        color.a=0;

        DOTween.To(()=>img.color,a=>img.color=a,color,0.5f).SetEase(Ease.InBack);
        DOTween.To(()=>this.transform.position,x=>this.transform.position=x,new Vector3(goldPosition.x,goldPosition.y+=100,goldPosition.z),0.5f).OnComplete(()=>
        {

            Gold.Instance.SpendGold();
            Gold.Instance.PingPang();

            Destroy(this.gameObject);

        }).SetEase(Ease.InBack);
    }
}
