using UnityEngine;

public class PNLevel1 : MonoBehaviour
{
    public GameObject gm;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.SetActive(true);
        }
    }


}
