using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {


    int index;
    private void Start()
    {
        index = transform.GetSiblingIndex();
        transform.GetChild(0).GetComponent<Text>().text = (index + 1).ToString();
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Level_" + (index + 1));
    }
}
