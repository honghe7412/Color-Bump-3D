using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeGame : MonoBehaviour 
{
    public Material obstacleMaterial;
    public Shader shaderStandard, legacySpectacular;
    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        obstacleMaterial.shader = legacySpectacular;
#else
        obstacleMaterial.shader = shaderStandard;
#endif
        int level = Mathf.Min(Prefs.UnlockedLevel, Const.TOTAL_LEVEL);
        print(Prefs.UnlockedLevel);
        SceneManager.LoadScene("Level_" + level);
        
        //Prefs.GoldManage=10;
    }
}
