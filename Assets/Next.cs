using UnityEngine;

public class Next : MonoBehaviour
{
    public GameObject gm;
    public GameObject gm1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.SetActive(false);
        }
         if(other.CompareTag("Level2"))
        {
            gm1.SetActive(true);
        }
    }


}
