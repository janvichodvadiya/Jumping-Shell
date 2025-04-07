using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject[] levels; 
    private int currentLevelIndex = 0;

    void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == 0);
        }
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex < levels.Length - 1)
        {
            levels[currentLevelIndex].SetActive(false); 
            currentLevelIndex++; 
            levels[currentLevelIndex].SetActive(true); 
        }
        else
        {
            Debug.Log(" Game Over or Restart");
        }
    }
}
