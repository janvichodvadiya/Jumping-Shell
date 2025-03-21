using UnityEngine;

public class LevelScene : MonoBehaviour
{
    public GameObject[] Levels;


    void Start()
    {
        Levels[LevelSelectMenu.CurrentLevel].SetActive(true);
    }
}
