using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pol : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed, JumpForce;
    AudioSource Jump0;

    private void Start()
    {
        Jump0 = GetComponent<AudioSource>();
    }
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horizontal, 0) * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump0.Play();
            rb.AddForce(new Vector2(0, 1) * JumpForce, ForceMode2D.Impulse);
        }
    }
}