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
        Jump();
    }

    void Move()
    {
           float Xmove = Input.GetAxis("Horizontal");
        float Ymove = Input.GetAxis("Vertical");
        rb.velocity += new Vector2(Xmove, Ymove) * Time.deltaTime * Speed;

        //float xMovement = joystick.Horizontal;
        //float yMovement = joystick.Vertical;

        //Vector2 moveDirection = new Vector2(xMovement, yMovement) * Speed;
        //rb.velocity = new Vector2(moveDirection.x, rb.velocity.y);
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


/*
 //New
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Player : MonoBehaviour
{
    public float Speed, JumpForce;
    public Rigidbody2D rb;
    private bool canJump = true;

    public GameObject smallPlayerPrefab;
    public GameObject fallingFramePrefab;
    public float smallPlayerOffset = 0.5f;
    public float frameFallSpeed = -5f;

    private GameObject activeFrame;
    private bool hasFrame = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float xMovement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xMovement * Speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                rb.AddForce(new Vector2(0, 1) * JumpForce, ForceMode2D.Impulse);
                canJump = false;
            }
            else if (!hasFrame)
            {
                DropFrameAndSpawnSmallPlayer();
            }
        }
    }

    private void DropFrameAndSpawnSmallPlayer()
    {
        Vector3 spawnPosition = transform.position;
        Vector2 currentVelocity = rb.velocity;

        activeFrame = Instantiate(fallingFramePrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D frameRb = activeFrame.GetComponent<Rigidbody2D>();

        Vector3 smallPlayerPosition = spawnPosition + new Vector3(0, smallPlayerOffset, 0);
        GameObject smallPlayer = Instantiate(smallPlayerPrefab, smallPlayerPosition, Quaternion.identity);
        Rigidbody2D smallRb = smallPlayer.GetComponent<Rigidbody2D>();

        if (frameRb != null)
        {
            frameRb.velocity = new Vector2(currentVelocity.x, frameFallSpeed);
        }
        if (smallRb != null)
        {
            smallRb.velocity = new Vector2(currentVelocity.x, frameFallSpeed);
        }

        activeFrame.tag = "Frame";
        hasFrame = true;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    public void Respawn(Vector3 position)
    {
        transform.position = position;
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        hasFrame = false;
        gameObject.SetActive(true);
    }
}
*/
/*
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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(currentSize, currentSize, 1);
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
        //  rb.velocity = new Vector2(Xmove * moveSpeed, rb.velocity.y);
        rb.velocity += new Vector2(Xmove, Ymove) * Time.deltaTime * Speed;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || jumpCount < maxJumps)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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

        if (collision.gameObject.CompareTag("Frame"))
        {
            Debug.Log("Player collided with a frame!");
            GameObject frame = collision.gameObject;
            float frameSize = frame.transform.localScale.x;

            if (CanCollectFrame(frameSize))
            {
                Debug.Log("Frame collected!");
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
            IsDroped = true; // Prevent multiple drops at once

            // Instantly reduce the player's size
            if (currentSize == 0.7f)
            {
                currentSize = 0.4f; // Shrink directly to smallest
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

            transform.localScale = new Vector3(currentSize, currentSize, 1); // Apply new size
            transform.position += new Vector3(0, 0.2f, 0); // Move player up slightly

            StartCoroutine(DropFramesWithDelay());
        }
    }


    IEnumerator DropFramesWithDelay()
    {
        GameObject firstFrame = sizeFrames.Pop();
        DropFrame(firstFrame, 0f); // Drop first frame

        yield return new WaitForSeconds(0.3f); // Wait before dropping the second frame

        if (sizeFrames.Count > 0)
        {
            GameObject secondFrame = sizeFrames.Pop();
            DropFrame(secondFrame, 0.5f); // Drop second frame slightly higher
        }

        yield return new WaitForSeconds(0.5f); // Wait before allowing another drop
        IsDroped = false;
    }



    void DropFrame(GameObject frame, float heightOffset)
    {
        if (frame == null)
            return;

        frame.SetActive(true);
        frame.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f + heightOffset, transform.position.z);

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
/*
//1st no Copy
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 2; // Default max jumps
    public GameObject smallPlayerPrefab;
    public float Speed;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Stack<GameObject> sizeFrames = new Stack<GameObject>();
    private float currentSize = 0.4f;
    private int jumpCount = 0;
    private bool hasCollectedSmallFrame = false;
    private bool IsDroped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(currentSize, currentSize, 1);
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
                rb.AddForce(new Vector2(0, 1) * jumpForce, ForceMode2D.Impulse);
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
        hasCollectedSmallFrame = true;

        if (frameSize == 0.4f && currentSize == 0.4f)
        {
            currentSize = 0.6f;
            maxJumps = 3; // Increase max jumps when a frame is collected
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
            DropFrame(firstFrame, 0f, 0f); // Drop first frame at normal position

            if (sizeFrames.Count > 0)
            {
                GameObject secondFrame = sizeFrames.Pop();
                DropFrame(secondFrame, 0.3f, 0f); // Drop second frame on top of first frame
                currentSize = 0.4f;
                hasCollectedSmallFrame = false;
            }
            else
            {
                currentSize = 0.6f;
            }
        }

        if (sizeFrames.Count == 0)
        {
            maxJumps = 2; // Reset max jumps to normal
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