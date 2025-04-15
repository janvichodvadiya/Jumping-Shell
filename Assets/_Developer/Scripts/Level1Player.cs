/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 3;
    public GameObject smallPlayerPrefab;
    public float Speed;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Stack<GameObject> sizeFrames = new Stack<GameObject>();
    private float currentSize = 0.4f;
    private int jumpCount = 0;
    private bool hasCollectedSmallFrame = false;
    private bool IsDroped = false;
    AudioSource JumpSound;

    [SerializeField] FixedJoystick joystick;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(currentSize, currentSize, 1);
        JumpSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
           float Xmove = Input.GetAxis("Horizontal");
        float Ymove = Input.GetAxis("Vertical");
        rb.velocity += new Vector2(Xmove, Ymove) * Time.deltaTime * Speed;
     
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || jumpCount < maxJumps)
            {
                JumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                // rb.AddForce(new Vector2(0 , 1) * jumpForce , ForceMode2D.Impulse);
                jumpCount++;
                isGrounded = false;

                if (jumpCount == maxJumps)
                {
                    ShrinkPlayer();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("Frame") && !IsDroped)
        {
            GameObject frame = collision.gameObject;
            float frameSize = frame.transform.localScale.x;

            if (CanCollectFrame(frameSize))
            {
                CollectFrame(frame, frameSize);
            }
        }
    }

    void CollectFrame(GameObject frame, float frameSize)
    {
        sizeFrames.Push(frame);
        frame.SetActive(false);

        if (frameSize == 0.4f && currentSize == 0.4f)
        {
            currentSize = 0.6f;
            hasCollectedSmallFrame = true;
        }
        else if (frameSize == 0.5f && currentSize == 0.6f && hasCollectedSmallFrame)
        {
            currentSize = 0.7f;
        }

        transform.localScale = new Vector3(currentSize, currentSize, 1);
    }

    void ShrinkPlayer()
    {
        if (sizeFrames.Count > 0)
        {
            GameObject firstFrame = sizeFrames.Pop();
            IsDroped = true;
            DropFrame(firstFrame, 0f, 0f);

            if (currentSize == 0.7f && sizeFrames.Count > 0)
            {
                GameObject secondFrame = sizeFrames.Pop();
                DropFrame(secondFrame, 0.3f, firstFrame.transform.position.x - transform.position.x);
                currentSize = 0.4f;
                hasCollectedSmallFrame = false;
            }
            else if (currentSize == 0.6f)
            {
                currentSize = 0.4f;
                hasCollectedSmallFrame = false;
            }
            else
            {
                currentSize = 0.6f;
            }
        }

        transform.localScale = new Vector3(currentSize, currentSize, 1);
    }

    void DropFrame(GameObject frame, float heightOffset, float xOffset)
    {
        if (frame == null)
            return;

        frame.SetActive(true);
        frame.transform.position = new Vector3(transform.position.x + xOffset, transform.position.y - 0.5f + heightOffset, transform.position.z);

        Rigidbody2D frameRb = frame.GetComponent<Rigidbody2D>();
        if (frameRb == null)
        {
            frameRb = frame.AddComponent<Rigidbody2D>();
        }

        frameRb.bodyType = RigidbodyType2D.Dynamic;
        frameRb.gravityScale = 5.0f;
        frameRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        frameRb.velocity = Vector2.zero;

        StartCoroutine(ResetDropFlag());
    }

    IEnumerator ResetDropFlag()
    {
        yield return new WaitForSeconds(0.5f);
        IsDroped = false;
    }

    bool CanCollectFrame(float frameSize)
    {
        if (frameSize == 0.4f && currentSize == 0.4f)
            return true;
        if (frameSize == 0.5f && currentSize == 0.6f && hasCollectedSmallFrame)
            return true;
        return false;
    }
}
*/

/*  // JOystick move
float xMovement = joystick.Horizontal;
float yMovement = joystick.Vertical;

Vector2 moveDirection = new Vector2(xMovement, yMovement) * Speed;
rb.velocity = new Vector2(moveDirection.x, rb.velocity.y);
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 3;
    public GameObject smallPlayerPrefab;
    public float Speed;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Stack<GameObject> sizeFrames = new Stack<GameObject>();
    private float currentSize = 0.4f;
    private int jumpCount = 0;
    private bool hasCollectedSmallFrame = false;
    private bool IsDroped = false;
    AudioSource JumpSound;

    [SerializeField] FixedJoystick joystick;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(currentSize, currentSize, 1);
        JumpSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        // Jump method is not needed in Update anymore because it's triggered by the UI button
    }

    void Move()
    {
        float Xmove = Input.GetAxis("Horizontal");
        float Ymove = Input.GetAxis("Vertical");
        rb.velocity += new Vector2(Xmove, Ymove) * Time.deltaTime * Speed;

        /*
        float xMovement = joystick.Horizontal;
        float yMovement = joystick.Vertical;

        Vector2 moveDirection = new Vector2(xMovement, yMovement) * Speed;
        rb.velocity = new Vector2(moveDirection.x, rb.velocity.y);
        */
    }

    // Public method to be called by the UI Button to trigger Jump
    public void OnJumpButtonPressed()
    {
        if (isGrounded || jumpCount < maxJumps)
        {
            JumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // rb.AddForce(new Vector2(0 , 1) * jumpForce , ForceMode2D.Impulse);
            jumpCount++;
            isGrounded = false;

            if (jumpCount == maxJumps)
            {
                ShrinkPlayer();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("Frame") && !IsDroped)
        {
            GameObject frame = collision.gameObject;
            float frameSize = frame.transform.localScale.x;

            if (CanCollectFrame(frameSize))
            {
                CollectFrame(frame, frameSize);
            }
        }
    }

    void CollectFrame(GameObject frame, float frameSize)
    {
        sizeFrames.Push(frame);
        frame.SetActive(false);

        if (frameSize == 0.4f && currentSize == 0.4f)
        {
            currentSize = 0.6f;
            hasCollectedSmallFrame = true;
        }
        else if (frameSize == 0.5f && currentSize == 0.6f && hasCollectedSmallFrame)
        {
            currentSize = 0.7f;
        }

        transform.localScale = new Vector3(currentSize, currentSize, 1);
    }

    void ShrinkPlayer()
    {
        if (sizeFrames.Count > 0)
        {
            GameObject firstFrame = sizeFrames.Pop();
            IsDroped = true;
            DropFrame(firstFrame, 0f, 0f);

            if (currentSize == 0.7f && sizeFrames.Count > 0)
            {
                GameObject secondFrame = sizeFrames.Pop();
                DropFrame(secondFrame, 0.3f, firstFrame.transform.position.x - transform.position.x);
                currentSize = 0.4f;
                hasCollectedSmallFrame = false;
            }
            else if (currentSize == 0.6f)
            {
                currentSize = 0.4f;
                hasCollectedSmallFrame = false;
            }
            else
            {
                currentSize = 0.6f;
            }
        }

        transform.localScale = new Vector3(currentSize, currentSize, 1);
    }

    void DropFrame(GameObject frame, float heightOffset, float xOffset)
    {
        if (frame == null)
            return;

        frame.SetActive(true);
        frame.transform.position = new Vector3(transform.position.x + xOffset, transform.position.y - 0.5f + heightOffset, transform.position.z);

        Rigidbody2D frameRb = frame.GetComponent<Rigidbody2D>();
        if (frameRb == null)
        {
            frameRb = frame.AddComponent<Rigidbody2D>();
        }

        frameRb.bodyType = RigidbodyType2D.Dynamic;
        frameRb.gravityScale = 5.0f;
        frameRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        frameRb.velocity = Vector2.zero;

        StartCoroutine(ResetDropFlag());
    }

    IEnumerator ResetDropFlag()
    {
        yield return new WaitForSeconds(0.5f);
        IsDroped = false;
    }

    bool CanCollectFrame(float frameSize)
    {
        if (frameSize == 0.4f && currentSize == 0.4f)
            return true;
        if (frameSize == 0.5f && currentSize == 0.6f && hasCollectedSmallFrame)
            return true;
        return false;
    }
}
