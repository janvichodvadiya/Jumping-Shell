using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float x;
    public float y;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = new Vector3 (x, y); 
    }
}

