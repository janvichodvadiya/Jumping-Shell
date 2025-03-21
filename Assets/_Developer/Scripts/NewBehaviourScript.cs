using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxShells = 2;
    private int currentShells;
    private Rigidbody2D rb;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject shellPrefab;
    private GameObject droppedShell;
    public GameObject redObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentShells = maxShells;
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (currentShells > 0)
            {
                DropShell();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    void DropShell()
    {
        currentShells--;
        if (currentShells >= 0 && shellPrefab != null)
        {
            droppedShell = Instantiate(shellPrefab, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shell") && currentShells < maxShells)
        {
            Destroy(other.gameObject);
            currentShells++;
        }
        else if (other.CompareTag("Exit") && currentShells == 0)
        {
            Debug.Log("Level Complete!");
        }
        else if (other.CompareTag("RedObject"))
        {
            Destroy(other.gameObject);
            if (currentShells < maxShells)
            {
                currentShells++;
            }
        }
    }
}
