using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EZCameraShake;


public class GameController : MonoBehaviour
{
    //public GameObject gameOverScreen;
    public GameObject player, virtualPlayer;
    public Image handImage,singleImg,endlessImg;
    public SpriteRenderer arrowLeft, arrowRight;
    public Material obstacleMaterial, planeMaterial, defaultMaterial,textMaterial;
    public GameObject gameOverOverlay;
    public CameraController mainCamera;
    public TextMesh stageText,SwipeToStartText;
    public CameraShaker cameraShaker;
    public GameObject levelButtonPrefab;
    public Transform levelContainer;
    public GameObject levelSelectorScreen;
    public GameObject menuObject;
    public GameObject gameOverContent,gameOverScreen;
    [SerializeField]
    private GameObject completeLevel,completeLevelContent;
    public Toggle playerNeverDie;
    public GameObject fpsCounter;
    
    [SerializeField]
    private GameObject planePrefab;

    private GameObject planeObject;

    [Header("Color Manager")]
    public Color[] obstacleColors;

    [Header("")]
    public bool enableTest;
    public float startPlayerFromZ;

    [HideInInspector]
    public bool isGameOver;

    private bool arrowFading;
    private int sceneNumber;

    public bool isGameComplete;

    public static GameController instance;

    [SerializeField]
    private GameObject DestinationLine;

    [SerializeField]
    private GameObject addGold;

    [SerializeField]
    private GameObject transformCanvas;

    public GameObject reminder;
    private bool reminderBool;

    [SerializeField]
    private Image progressImg,progressBg;

    [SerializeField]
    private Text progressTxt1,progressTxt2;
    string[] strs;

    [SerializeField]
    private Text recordText,recordTextOver;
    private void Awake()
    {
        if(reminder)
            reminder.SetActive(false);
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        
        if(GameMaster.instance.IsRevive)
        {
            //GameMaster.instance.IsRevive=false;
            handImage.gameObject.SetActive(false);
            singleImg.gameObject.SetActive(false);
            endlessImg.gameObject.SetActive(false);
            SwipeToStartText.gameObject.SetActive(false);
        }

        #if UNITY_EDITOR
            if (enableTest)
            {
                var position = player.transform.position;
                mainCamera.offset = mainCamera.transform.position - position;
                player.transform.position = new Vector3(position.x, position.y, startPlayerFromZ);
                virtualPlayer.transform.position = player.transform.position;
                mainCamera.transform.position = player.transform.position + mainCamera.offset;
            }
        #endif
        
        if (GameState.numPlayed != 0)
        {
           /*  Color[] planeColors = { new Color(205/255f, 26/255f, 27/255f), new Color(212/255f, 83/255f, 32/255f), new Color(226/255f, 197/255f, 64/255f),
                new Color(50/255f, 50/255f, 50/255f), new Color(88/255f, 49/255f, 136/255f), new Color(88/255f, 126/255f, 236/255f),
                //new Color(0.113f, 0.113f, 0.113f), new Color(0.594f, 0.0f, 0.131f), new Color(0.085f, 0.0f, 0.377f), new Color(220/255f, 171/255f, 201/255f), 
                //new Color(0.0f, 0.169f, 0.547f), 
                new Color(0.557f, 0.478f, 0.0f), new Color(0.528f, 0.159f, 0.0f), new Color(0.396f, 0.0f, 0.343f),
                new Color(0.0f, 0.491f, 0.418f), new Color(0.538f, 0.229f, 0.1f)};*/
            
            int index = Random.Range(1, obstacleColors.Length);
            int index2 = 0;
            int index3 = 0;

            while (true)
            {
                index2 = Random.Range(0, obstacleColors.Length);
                index3 = Random.Range(0, obstacleColors.Length);
                if (index != index2&&index!=index3&&index2!=index3) break;
            }

            if(sceneName!=GameMaster.instance.LevelName)
            {
                planeMaterial.color = obstacleColors[index];
                obstacleMaterial.color = obstacleColors[index2];
                defaultMaterial.color=obstacleColors[index3];
            }
        }
        
        GameMaster.instance.LevelName=sceneName;
        //print(sceneName);
        strs=sceneName.Split('_');
        
        if(strs[0]=="Level")
        {
            sceneNumber = int.Parse(strs[1]);

            Color bgColor=planeMaterial.color;
            //bgColor.a=0.5f;
            progressBg.color=bgColor;

            progressImg.color=defaultMaterial.color;
            //progressTxt1.color=progressTxt2.color=planeMaterial.color;
        }
        else
        {
            addPlane();
        }
        
        stageText.text = "关卡 " + sceneNumber;

        for(int i = 0; i < Const.TOTAL_LEVEL; i++)
        {
            var button = Instantiate(levelButtonPrefab, levelContainer);
            button.transform.localScale = Vector3.one;
        }

        //menuObject.SetActive(GameConfig.instance.enableTesting);
        //fpsCounter.SetActive(GameConfig.instance.enableTesting);
        #if UNITY_EDITOR
            menuObject.SetActive(true);
        #endif 

        playerNeverDie.isOn = Prefs.PlayerNeverDie;

        //Music.instance.PlayAMusic();
        GameState.numPlayed++;

        if(recordText)
        {
            recordTextOver.text=Prefs.OnRecord.ToString()+"m";
        
            recordText.text="记录:"+Prefs.OnRecord.ToString()+"m";
        }
    }

    private void addPlane()
    {
        if (planeObject==null)
        {
            planeObject=Instantiate(planePrefab);
            planeObject.transform.SetParent(this.transform.parent);
        }
        else
        {
            Vector3 positionObj=planeObject.GetComponent<AddPlane>().GetPosition;
            
            planeObject=Instantiate(planePrefab,positionObj,Quaternion.identity);
            planeObject.transform.SetParent(this.transform.parent);
            DestinationLine.transform.position=new Vector3(0,0,planeObject.transform.position.z);
        }
    }

    public void OnGameMasterCall()
    {
        print("OnGameMasterCall");

        handImage.gameObject.SetActive(false);
    }

    public void StartPlaying()
    {
        virtualPlayer.GetComponent<ConstantForce>().enabled = true;
        handImage.CrossFadeAlpha(0, 0.3f, true);

        singleImg.CrossFadeAlpha(0,0.3f,true);
        singleImg.raycastTarget=false;
        endlessImg.CrossFadeAlpha(0,0.3f,true);
        endlessImg.raycastTarget=false;

        arrowFading = true;
    }

    public void GameOver()
    {
        isGameOver = true;
        virtualPlayer.GetComponent<ConstantForce>().force = Vector3.zero;
        Music.instance.Pause();

        Timer.Schedule(this, 1.5f, () =>
        {
            virtualPlayer.GetComponent<ConstantForce>().force = Vector3.forward * 4f;
            gameOverScreen.SetActive(true);
            gameOverOverlay.GetComponent<ImageFader>().Fade(0, 0.6f, 0.3f);

            gameOverContent.transform.localPosition = new Vector3(0, -800, 0);
            iTween.MoveTo(gameOverContent, iTween.Hash("y", -250, "islocal", true, "time", 0.2f));

            CUtils.ShowInterstitialAd();
        });

        Timer.Schedule(this, 7f, () =>
        {
            virtualPlayer.GetComponent<ConstantForce>().enabled = false;
        });

        cameraShaker.ShakeOnce(1f, 0.2f, 0.1f, 1f);
    }

    public void Reminder()
    {
        reminderBool=true;

        reminder.SetActive(true);

        Timer.Schedule(this,2.0f,()=>
        {
            reminder.SetActive(false);
        });
    }

    public void GameComplete()
    {
        if(strs[0]!="Level")
        {
            addPlane();
            return;
        }

        isGameComplete=true;
        
        if(completeLevel)
        {
            completeLevel.SetActive(true);
            completeLevel.transform.GetChild(0).gameObject.GetComponent<Text>().text="第"+strs[1]+"关";
            completeLevelContent.transform.localPosition = new Vector3(0, -800, 0);
            iTween.MoveTo(completeLevelContent, iTween.Hash("y", -200, "islocal", true, "time", 0.2f));
        }
        
        PlayerController.instance.enabled = false;
        virtualPlayer.GetComponent<ConstantForce>().enabled = false;
        Music.instance.Pause();

        if (Prefs.UnlockedLevel == sceneNumber) Prefs.UnlockedLevel++;

        /* Timer.Schedule(this, 3.5f, () =>
        {
            NextLevel();
        });*/
    }

    public void NextLevel(int addGoldNum)
    {
        if(transformCanvas)
        {
            completeLevel.SetActive(false);

            StartCoroutine(addGoldA(addGoldNum));
        }
    } 

    public void OnaddGold(int addGoldNum)
    {
        StartCoroutine(addGoldA(addGoldNum,false));
    }

    IEnumerator addGoldA(int Int,bool isLoadScene=true)
    {
        while (Int>0)
        {
            Int--;
            Instantiate(addGold,transformCanvas.transform).gameObject.GetComponent<Addgold>().OnAddGold();

            yield return new WaitForSeconds(0.2f);

            if(Int==0)
            {
                if(isLoadScene)
                {
                    Timer.Schedule(this, 1.0f, () =>
                    {
                        int nextLevel = Mathf.Min(Const.TOTAL_LEVEL, sceneNumber + 1);
                        SceneManager.LoadScene("Level_" + nextLevel);
                    });
                }
            }
        }
    }
    
    IEnumerator SpendGoldA(int Int)
    {
        while (Int>0)
        {
            Int--;

            Instantiate(addGold,transformCanvas.transform).gameObject.GetComponent<Addgold>().OnSpendGold();

            yield return new WaitForSeconds(0.2f);

            if(Int==0)
            {
                Timer.Schedule(this, 0.5f, () =>
                {
                    if(reminderBool)
                    {
                        gameOverContent.SetActive(true);

                        reminderBool=false;

                        return;
                    }

                    CUtils.ReloadScene();
                });
            }
        }
    }

    public void OnMenuOpen()
    {
        levelSelectorScreen.SetActive(true);
    }

    public void OnMenuClose()
    {
        levelSelectorScreen.SetActive(false);
    }

    public void RateGame()
    {
        CUtils.OpenStore();
    }

    public void GameMode(string modeName)
    {
        if(strs[0]==modeName)
        {
            //GameModes.Instance.OnBack();
        }
        else
        {
            if(modeName=="Level")
            {
                int level = Mathf.Min(Prefs.UnlockedLevel, Const.TOTAL_LEVEL);
                
                SceneManager.LoadScene("Level_" + level);
            }

            if(modeName=="Endless")
            {
                SceneManager.LoadScene("Endless_Mode");

                GameMaster.instance.CurrentLevelInt=int.Parse(strs[1]);
            }
        }
    }

    public void Replay(int SpendGoldNum)
    {
        /* if(strs[0]=="Endless")
        {
            CUtils.ReloadScene();
            return;
        }*/
        
        if(Prefs.GoldManage<SpendGoldNum&&SpendGoldNum>0)
        {
            if(!reminder.activeSelf)
                Reminder();
            
            return;
        }

        gameOverScreen.SetActive(false);

        if(SpendGoldNum<=0)
        {
            CUtils.ReloadScene();

            return;
        }

        StartCoroutine(SpendGoldA(SpendGoldNum));
    }

    public void Revive() //无尽模式
    {
        //PlayerController.instance.Revive();
        ///gameOverScreen.SetActive(false);
        CUtils.ReloadScene();
        
        GameMaster.instance.IsRevive=true;
    }

    public void OnPlayerNeverDieValueChanged()
    {
        Prefs.PlayerNeverDie = playerNeverDie.isOn;
    }

    private float lastEscapeTime = -10;
    private void Update()
    {
        if (arrowFading)
        {
            if (arrowLeft.color.a < 0.0001f)
            {
                arrowFading = false;
            }
            else
            {
                var color = arrowLeft.color;
                color.a = Mathf.Lerp(color.a, 0, Time.deltaTime * 7);
                arrowLeft.color = color;
                arrowRight.color = color;
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.time - lastEscapeTime < 4)
            {
                Application.Quit();
            }
            else
            {
                Toast.instance.ShowMessage("Press back again to quit game", 2);
                lastEscapeTime = Time.time;
            }
        }
    }
}
