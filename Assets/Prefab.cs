using System.Collections.Generic;
using UnityEngine;

public class Prefab : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 2;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Stack<Vector2> jumpStack = new Stack<Vector2>(); // Stack for jumps
    private int sizeStack = 0;  // Stack system for layers
    public int maxSize = 2;  // Max shell layers

  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpStack.Count < maxJumps))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpStack.Push(rb.velocity); // Store jump in stack

            if (jumpStack.Count == maxJumps)
            {
                Debug.Log("Double Jump Reached!");
                jumpStack.Pop(); // Remove last jump
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpStack.Clear(); // Reset stack on ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void IncreaseSize()
    {
        if (sizeStack < maxSize)
        {
            sizeStack++;
            transform.localScale += new Vector3(0.2f, 0.2f, 0); // Increase size
            Debug.Log("Player Size Increased: " + sizeStack);
        }
    }
}
