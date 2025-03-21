using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels; // All levels in an array
    private int currentLevel = 0; // Start from Level 0

    void Start()
    {
        // Ensure only the first level is active at the start
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Portal"))
        {
            NextLevel();
        }
    }

    void NextLevel()
    {
        if (currentLevel < levels.Length - 1)
        {
            levels[currentLevel].SetActive(false); // Disable current level
            currentLevel++; // Move to next level
            levels[currentLevel].SetActive(true); // Enable next level
        }
    }
}
