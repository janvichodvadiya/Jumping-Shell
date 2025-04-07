using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    public Button[] Level; 
    public static int CurrentLevel;
    public static int UnlockLevel;

   /* private void Awake()
    {
        UnlockLevel = PlayerPrefs.GetInt("UnlockLevel", 1);

        for (int i = 0; i < Level.Length; i++)
        {
            Level[i].interactable = false; 
        }

        for (int i = 0; i < UnlockLevel; i++)
        {
            Level[i].interactable = true; 
        }
    }*/

    public void OnClick(int LevelNum)
    {
        CurrentLevel = LevelNum;
        SceneManager.LoadScene(1);
    }
}
