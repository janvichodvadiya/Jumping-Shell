using UnityEngine;

public class JoyStickJump : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    public int maxJumps = 3;         
    private int jumpCount = 0;       

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void JumpNow()
    {
        if (jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0; 
        }
    }
}
