using UnityEngine;

public class Level1Player : MonoBehaviour
{
    public GameObject miniPlayerPrefab; // Mini player nu prefab
    public GameObject framePrefab; // Frame ni broken parts
    public Transform spawnPoint; // Mini player spawn position

    private Rigidbody2D rb;
    private bool hasSplit = false; // Ensure only one split
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Space press kare & already split na thayu hoy to execute
        if (Input.GetKeyDown(KeyCode.Space) && !hasSplit)
        {
            SplitPlayer();
        }
    }

    void SplitPlayer()
    {
        hasSplit = true;

        // 1️Big Player Freeze (Completely stop movement)
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // 2 Mini Player Spawn
        Instantiate(miniPlayerPrefab, spawnPoint.position, Quaternion.identity);

        // 3️ Frame realistically fall karva spawn karo
        GameObject brokenFrame = Instantiate(framePrefab, transform.position, Quaternion.identity);
        Rigidbody2D frameRb = brokenFrame.GetComponent<Rigidbody2D>();

        // **Randomized fall effect**
        frameRb.velocity = new Vector2(Random.Range(-1f, 1f), -2f);
        frameRb.angularVelocity = Random.Range(-200f, 200f);

        // 4 Big Player Hide (but still in scene)
        gameObject.SetActive(false);
    }
}
