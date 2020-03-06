using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Progress : MonoBehaviour
{
    public static Progress Instance;

    [SerializeField]
    private Text progressText;

    [SerializeField]
    private Image progressBar,levelBar;
    private Color color;

    private Vector2 progressPosition;
    private Vector2 propressRect;

    [SerializeField]
    private Text cueerntLevelText,nextLevelText,LevelText;
    void Awake()
    {
        if(Instance==null)
            Instance=this;

        progressBar.fillAmount=levelBar.fillAmount=0;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int currentInt=int.Parse(sceneName.Split('_')[1]);
        int nextText=currentInt+1;
        
        cueerntLevelText.text=LevelText.text=currentInt.ToString();
        nextLevelText.text=nextText.ToString();
    }

    public void ShowProperss(float properssNum)
    {
        int progressInt=Mathf.Clamp((int)(100*properssNum),0,100);
        progressText.text=progressInt.ToString()+"%";
        progressBar.fillAmount=levelBar.fillAmount=properssNum;
        //StopCoroutine(HideProperss());

       // color.a=1;

        //DOTween.To(()=>progressText.color,a=>progressText.color=a,color,1);

        //StartCoroutine(HideProperss());
    }

    IEnumerator HideProperss()
    {
        yield return new WaitForSeconds(3f);

        color.a=0;

        DOTween.To(()=>progressText.color,a=>progressText.color=a,color,1);
    }

    // Update is called once per frame
   /*void Update()
    {
        
    }*/
}
