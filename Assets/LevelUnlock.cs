using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    int UnlockLevelsNumber;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("LevelsUnlocked", 1);
        }
        UnlockLevelsNumber = PlayerPrefs.GetInt("UnlockLevels");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

    private void Update()
    {
        UnlockLevelsNumber = PlayerPrefs.GetInt("UnlockLevels");

        for (int i = 0; i < UnlockLevelsNumber; i++)
        {
            buttons[i].interactable = true;
        }
    }
}
