using UnityEngine;

public class frame : MonoBehaviour
{
    public GameObject bigPlayerPrefab; // Big Player nu Prefab

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Mini Player frame touch kare
        {
            Transform playerTransform = other.transform;

            //  Mini Player ne destroy karo
            Destroy(other.gameObject);

            //  Big Player ne respawn karo frame ni position par
            Instantiate(bigPlayerPrefab, playerTransform.position, Quaternion.identity);

            //  Frame ne destroy karo
            Destroy(gameObject);
        }
    }
}
