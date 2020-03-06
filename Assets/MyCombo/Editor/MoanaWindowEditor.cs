using UnityEngine;
using UnityEditor;

public class MoanaWindowEditor
{
    [MenuItem("Moana Games/Reset game")]
    static void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}