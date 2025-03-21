using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Levels;
    void Start()
    {
        Levels[LevelSelectMenu.CurrentLevel].SetActive(true);
    }
}
