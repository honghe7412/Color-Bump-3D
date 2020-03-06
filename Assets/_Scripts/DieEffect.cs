using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DieEffect : MonoBehaviour
{
    public static DieEffect instance;
    [SerializeField]
    private Image dieEffect;

    void Awake()
    {
        if(instance==null)
            instance=this;

        Color color=dieEffect.color;
        color.a=0;
        dieEffect.color=color;
    }

    public void OnDieEffect()
    {
        Color color=dieEffect.color;
        color.a=1.0f;

        DOTween.To(()=>dieEffect.color,a=>dieEffect.color=a,color,0.2f).OnComplete(()=>
        {
            color.a=0f;

            DOTween.To(()=>dieEffect.color,a=>dieEffect.color=a,color,0.5f)/* .OnComplete(()=>{Time.timeScale=1f;})*/;
        });
    }
}
