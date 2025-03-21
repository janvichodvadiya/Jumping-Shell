using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill : MonoBehaviour
{
    public int ReSpwan;

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Pokie"))
        {
            SceneManager.LoadScene(ReSpwan);
        }
    }
}
