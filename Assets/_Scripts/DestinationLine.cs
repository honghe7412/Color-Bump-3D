using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DestinationLine : MonoBehaviour
{
    private float distance;
    private float properssNum;
    float remDistance;
    private AudioSource endSound;
    private string sceneName;

    [SerializeField]
    private ParticleSystem winParticle;
    private void Start()
    {
        remDistance=0;
        
        distance=transform.position.z-PlayerController.instance.transform.position.z;
        
        sceneName=SceneManager.GetActiveScene().name;
        sceneName = sceneName.Split('_')[0];

        //endSound=this.gameObject.AddComponent<AudioSource>();
        //endSound.playOnAwake=false;
        //endSound.clip=clip;
    }
    private void Update()
    {
        if(sceneName=="Level") //非无尽模式
        {
            remDistance=transform.position.z-PlayerController.instance.transform.position.z;
            properssNum=((1-remDistance/distance));

            OnProperss(properssNum);
        }

        if (PlayerController.instance.transform.position.z > transform.position.z)
        {
            if (PlayerController.instance.transform.position.y > -1f)
            {
                //print(GameController.instance.isGameComplete);
                GameController.instance.GameComplete();

                if(sceneName=="Level")
                {
                    Sound.instance.Play(Sound.Others.EndSound);
                    //endSound.Play();
                    winParticle.Play();
                    enabled = false;
                }

                //print(GameController.instance.isGameComplete);
            }
        }

        if (GameController.instance.isGameOver)
        {
            if (GameController.instance.virtualPlayer.transform.position.z > transform.position.z - 10)
            {
                GameController.instance.virtualPlayer.GetComponent<ConstantForce>().enabled = false;
                enabled = false;
            }
        }
    }

    private void OnProperss(float _properssNum)
    {
        Progress.Instance.ShowProperss(_properssNum);
    }
}
