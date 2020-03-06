using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class InvincibleUI : MonoBehaviour
{
    public static InvincibleUI instance;

    void Awake()
    {
        if(instance==null)
            instance=this;
    }

    [SerializeField]
    private Image invincibleBar;

    [SerializeField]
    private GameObject btn;
    private float maxHight;
    private Vector2 sizeRect=Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        btn.SetActive(false);

        maxHight=invincibleBar.rectTransform.sizeDelta.y;
        sizeRect.x=invincibleBar.rectTransform.sizeDelta.x;
        invincibleBar.rectTransform.sizeDelta=sizeRect;
        //DieEffect();
    }

    public void SetProgress(float setNum)
    {
        sizeRect.y+=setNum;
        sizeRect.y=Mathf.Clamp(sizeRect.y,0,maxHight);

        invincibleBar.rectTransform.sizeDelta=sizeRect;

        if(sizeRect.y/maxHight>=1)
        {
            btn.SetActive(true);
        }
    }

    public void TiggerInvincible()
    {
        btn.SetActive(false);
        Vector2 size=new Vector2(sizeRect.x,0);
        DOTween.To(()=>invincibleBar.rectTransform.sizeDelta,a=>invincibleBar.rectTransform.sizeDelta=a,size,10.0f).OnComplete(()=>
        {
            sizeRect=size;
        });
    }
}
