using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameModes : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> buttons;
    private List<Vector3> firstPosition=new List<Vector3>();

    private bool show;

    private Image thisImg;

    private float speed=0.3f;

    public static GameModes Instance;

    [SerializeField]
    private Image bgImg;
    void Awake()
    {
        if(Instance==null)
            Instance=this;
    }
    void Start()
    {
        bgImg.raycastTarget=false;

        thisImg=this.GetComponent<Image>();

        for (int i = 0; i < buttons.Count; i++)
        {
            firstPosition.Add(buttons[i].transform.position);
            buttons[i].transform.position=this.transform.position;
            buttons[i].transform.localScale=Vector3.zero;
        }
    }

    public void OnClick()
    {
        if(show)
            return;

        bgImg.raycastTarget=true;

        show=true;

        for (int i = 0; i < buttons.Count; i++)
        {
            GameObject go=buttons[i];
            childDoTween(go,firstPosition[i],new Vector3(1,1,1));
        }

        ThisDoTween(15f,0);
    }

    public void OnBack()
    {
        Time.timeScale=1;
        show=false;
        bgImg.raycastTarget=false;
        
        for (int i = 0; i < buttons.Count; i++)
        {
            GameObject go=buttons[i];
            childDoTween(go,this.transform.position,new Vector3(-1,-1,-1));
        }

        ThisDoTween(-15f,1);
    }
    
    private void ThisDoTween(float byMoveX,float alphaNum)
    {
        Vector3 thisByPosition=new Vector3(this.transform.position.x+byMoveX,this.transform.position.y,this.transform.position.z);

        DOTween.To(()=>this.transform.position,p=>this.transform.position=p,thisByPosition,speed).OnComplete(()=>
        {
            if(alphaNum==0)
                Time.timeScale=0;
        });

        Color color=thisImg.color;
        color.a=alphaNum;
        DOTween.To(()=>thisImg.color,p=>thisImg.color=p,color,speed);
    }

    private void childDoTween(GameObject go,Vector3 position,Vector3 byScale)
    {
        go.transform.DOBlendableScaleBy(byScale,speed);
        DOTween.To(()=>go.transform.position,p=>go.transform.position=p,position,speed);
    }
}
