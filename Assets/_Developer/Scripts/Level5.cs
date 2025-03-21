using UnityEngine;

public class Level5 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public GameObject smallPlayerPrefab; // Assign small player prefab in Inspector
    public GameObject redObject; // Assign red object in Inspector

    private Rigidbody2D rb;
    private int jumpCount = 0;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Player movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Double Jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }

        // Reset jump count when grounded
        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }

        // If touching red object, transform into small player
        if (collision.gameObject == redObject)
        {
            TransformToSmallPlayer();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void TransformToSmallPlayer()
    {
        if (smallPlayerPrefab != null)
        {
            // Spawn small player at the same position
            GameObject smallPlayer = Instantiate(smallPlayerPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy big player
        }
    }
}
