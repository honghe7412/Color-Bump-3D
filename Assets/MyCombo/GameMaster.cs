using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster:MonoBehaviour
{
    public static GameMaster instance;

    private string levelName;
    private int currentLevelInt=1;
    
    private void Awake()
    {
        if (instance == null) instance = this;

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    public bool IsRevive{set;get;}

    public int Mileage{set;get;} 
    public string LevelName
    {
        set{levelName=value;}
        get{return levelName;}
    }
    public int CurrentLevelInt
    {
        set{currentLevelInt=value;}
        get{return currentLevelInt;}
    }
}
