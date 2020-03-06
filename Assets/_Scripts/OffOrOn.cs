using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SetupType
{
    Sound,
    Music
}
public class OffOrOn : MonoBehaviour
{
    private Button btn;

    [SerializeField]
    private GameObject btnOn;
    private bool off=false;

    [SerializeField]
    private SetupType setupType;
    void Start()
    {
        btn=this.GetComponent<Button>();
        
        if(setupType==SetupType.Sound)
        {
            btnOn.SetActive(Prefs.OnSound);
        }
        else if(setupType==SetupType.Music)
        {
            btnOn.SetActive(Prefs.OnMusic);
            Music.instance.setupMusic(Prefs.OnMusic);
        }

        btn.onClick.AddListener(OffAndOnSound);
    }

    public void OffAndOnSound()
    {
        if(btnOn.activeSelf)
        {
            btnOn.SetActive(false);
            Setup(false);
            off=true;
        }
        else
        {
            btnOn.SetActive(true);
            Setup(true);
            off=false;
        }
    }

    private void Setup(bool setupBool)
    {
        if(setupType==SetupType.Sound)
            Prefs.OnSound=setupBool;

        else if(setupType==SetupType.Music)
        {
            Prefs.OnMusic=setupBool;
            Music.instance.setupMusic(setupBool);
        }
    }
}
