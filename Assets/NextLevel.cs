using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] AudioSource Music;

    public void OnMusic()
    {
        Music.Play();
    }

    public void OffMusic()
    {
        Music.Stop();
    }
}
