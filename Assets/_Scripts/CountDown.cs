using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{
    [SerializeField]
    private Image resurgence;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private int intTime;

    [SerializeField]
    private GameObject btn;

    private bool isTime;

    private float currentTotal=0f;

    private int timeTotal;
    // Start is called before the first frame update
    void Start()
    {
        timeTotal=intTime;

        resurgence.fillAmount=0;

        timeText.text=intTime.ToString();

        StartCoroutine(a());

        btn.SetActive(false);

        isTime=true;
    }

    IEnumerator a()
    {
        while(intTime>0)
        {
            yield return new WaitForSeconds(1.0f);
            intTime--;
            timeText.text=intTime.ToString();

            if(intTime<=0)
                btn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isTime&&intTime>0)
        {
            currentTotal+=Time.deltaTime;
            resurgence.fillAmount=currentTotal/timeTotal;
        }
    }
}
