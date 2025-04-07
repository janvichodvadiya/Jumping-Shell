using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public GameObject Door;
    private void OnTriggerEnter2D(Collider2D colliosion)
    {
        if(colliosion.CompareTag("Player"))
        {
            Debug.Log("Key Picked Up");

            Door.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.SetActive(false);
        }
     
    }
}
