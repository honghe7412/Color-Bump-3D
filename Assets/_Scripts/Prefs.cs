using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefs 
{
    public static int UnlockedLevel
    {
        get { return PlayerPrefs.GetInt("unlocked_level", 1); }
        set { PlayerPrefs.SetInt("unlocked_level", value); }
    }

    public static bool PlayerNeverDie
    {
        get { return PlayerPrefs.GetInt("player_never_die") == 1 ? true : false; }
        set { PlayerPrefs.SetInt("player_never_die", value ? 1 : 0); }
    }
    
    public static int GoldManage
    {
        get{return PlayerPrefs.GetInt("Gold",10);}
        set{PlayerPrefs.SetInt("Gold",value);}
    }

    public static bool OnSound
    {
        get{return PlayerPrefs.GetInt("SoundSetup",1)==1?true:false;}
        set{PlayerPrefs.SetInt("SoundSetup",value?1:0);}
    }

    public static bool OnMusic
    {
        get{return PlayerPrefs.GetInt("MusicSetup",1)==1?true:false;}
        set{PlayerPrefs.SetInt("MusicSetup",value?1:0);}
    }

    public static int OnRecord
    {
        get{return PlayerPrefs.GetInt("Record",0);}
        set{PlayerPrefs.SetInt("Record",value);}
    }
}
