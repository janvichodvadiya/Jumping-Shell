
using UnityEngine;

public class RepeateBtn : MonoBehaviour
{
    public GameObject[] levels; 
    private Vector3[][] initialPositions;
    private GameObject[][] levelObjects;

    void Start()
    {
        int levelCount = levels.Length;
        initialPositions = new Vector3[levelCount][];
        levelObjects = new GameObject[levelCount][];

        for (int i = 0; i < levelCount; i++)
        {
            if (levels[i] != null)
            {
                int childCount = levels[i].transform.childCount;
                levelObjects[i] = new GameObject[childCount];
                initialPositions[i] = new Vector3[childCount];

                for (int j = 0; j < childCount; j++)
                {
                    levelObjects[i][j] = levels[i].transform.GetChild(j).gameObject;
                    initialPositions[i][j] = levelObjects[i][j].transform.position;
                }
            }
        }
    }

    public void ReplayLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].activeSelf) 
            {
                ResetLevel(i);
                return; 
            }
        }
    }

    void ResetLevel(int levelIndex)
    {
        for (int j = 0; j < levelObjects[levelIndex].Length; j++)
        {
            levelObjects[levelIndex][j].transform.position = initialPositions[levelIndex][j];
            levelObjects[levelIndex][j].SetActive(true); 
        }
    }
}
/*
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeateBtn : MonoBehaviour
{
    private Vector3 playerStartPos;
    private Dictionary<GameObject, Vector3> objectsStartPos = new Dictionary<GameObject, Vector3>();

    public GameObject player;
    public List<GameObject> resettableObjects; // Assign in Inspector

    private void Start()
    {
        // Save player start position
        playerStartPos = player.transform.position;

        // Save all objects' start positions
        foreach (GameObject obj in resettableObjects)
        {
            objectsStartPos[obj] = obj.transform.position;
        }
    }

    public void ReplayLevel()
    {
        // Reset player position
        player.transform.position = playerStartPos;

        // Reset all objects to their starting positions
        foreach (var obj in objectsStartPos)
        {
            obj.Key.transform.position = obj.Value;
        }

        Debug.Log("Level Restarted!");
    }
}
*/