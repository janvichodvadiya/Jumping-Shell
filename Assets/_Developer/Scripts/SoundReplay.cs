using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundReplay : MonoBehaviour
{
    public void MoveToScene(int _SceneId)
    {
        SceneManager.LoadScene(_SceneId);
    }
}
