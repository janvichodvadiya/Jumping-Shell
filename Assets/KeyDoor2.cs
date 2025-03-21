using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor2 : MonoBehaviour
{
    public GameObject Door;
    private void OnTriggerEnter2D(Collider2D colliosion)
    {
        if (colliosion.CompareTag("MiniPlayer"))
        {
            Debug.Log("Key Picked Up");

            Door.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.SetActive(false);
        }

    }
}
