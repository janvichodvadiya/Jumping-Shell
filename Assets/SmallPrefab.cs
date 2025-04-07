/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPrefab : MonoBehaviour
{
    private bool hasCollectedFrame = false; // Prevent multiple size changes
    public GameObject mainPlayerPrefab; // Reference to Main Player

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Frame") && !hasCollectedFrame) // ✅ Frame collected
        {
            GrowAndDestroyFrame(collision.gameObject);
        }
    }

    private void GrowAndDestroyFrame(GameObject frame)
    {
        hasCollectedFrame = true; // ✅ Prevent multiple size increases
        transform.localScale = new Vector3(0.5f, 0.5f, 1f); // ✅ Set size to exactly 0.5

        Destroy(frame); // ✅ Immediately destroy the frame
        RespawnMainPlayer(); // Activate Main Player Again
    }

    private void RespawnMainPlayer()
    {
        GameObject mainPlayer = Instantiate(mainPlayerPrefab, transform.position, Quaternion.identity);
        mainPlayer.SetActive(true);
        Destroy(gameObject); // Destroy Small Player
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPrefab : MonoBehaviour
{
    private bool hasCollectedFrame = false;
    public GameObject mainPlayerPrefab;
    private float currentSize; // Store current size

    private void Start()
    {
        currentSize = transform.localScale.x; // ✅ Read size from prefab (0.3 initially)
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Frame") && !hasCollectedFrame && gameObject.CompareTag("Smallplayer"))
        {
            float frameSize = collision.transform.localScale.x; // ✅ Get frame size
            bool sizeUpdated = UpdateSize(frameSize);
            if (sizeUpdated)
            {
                Destroy(collision.gameObject); // ✅ Delete frame only if size updated
            }
        }
    }

    private bool UpdateSize(float frameSize)
    {
        hasCollectedFrame = true;

        if (currentSize == 0.3f && frameSize == 0.1f)
        {
            currentSize = 0.4f;
        }
        else if (currentSize == 0.4f && frameSize == 0.4f)
        {
            currentSize = 0.5f;
        }
        else if (currentSize == 0.5f && frameSize == 0.4f)
        {
            currentSize = 0.6f;
        }
        else if (currentSize == 0.6f && frameSize == 0.5f)
        {
            currentSize = 0.7f;
        }
        else
        {
            return false; // ❌ Invalid frame collection
        }

        transform.localScale = new Vector3(currentSize, currentSize, 1f); // ✅ Update player size

        if (currentSize == 0.7f)
        {
            RespawnMainPlayer();
        }

        return true;
    }

    private void RespawnMainPlayer()
    {
        GameObject mainPlayer = Instantiate(mainPlayerPrefab, transform.position, Quaternion.identity);
        mainPlayer.tag = "Player"; // ✅ Ensure correct tag for main player
        mainPlayer.SetActive(true);
        Destroy(gameObject); // ✅ Destroy small player
    }
}
