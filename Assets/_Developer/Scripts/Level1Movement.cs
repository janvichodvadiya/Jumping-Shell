/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Movement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed, JumpForce;
    AudioSource Jump0;
    public GameObject SmallPrefab;

  private Stack<GameObject> SmallStack = new Stack<GameObject>();
    public void Small()
    {

    }
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
            rb.AddForce(new Vector2(0 , 1) * JumpForce, ForceMode2D.Impulse);
        }
    }
}
*/
using UnityEngine;

public class Level1Movement : MonoBehaviour
{
    public GameObject shell; // Assign shell in the inspector
    private Rigidbody2D rb;
    private bool isSmall = false;
    private bool canDoubleJump = true;

    public float jumpForce = 7f;
    public float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = true;
            }
            else if (canDoubleJump && !isSmall)
            {
                DetachShell();
            }
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1.1f);
    }

    void DetachShell()
    {
        shell.transform.parent = null;
        shell.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        isSmall = true;
        canDoubleJump = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == shell)
        {
            MergeShell();
        }
    }

    void MergeShell()
    {
        shell.transform.parent = transform;
        shell.transform.localPosition = Vector3.zero;
        shell.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        isSmall = false;
    }
}
