using UnityEngine;

public class PortalSound : MonoBehaviour
{
    public AudioClip portalSound; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            audioSource.PlayOneShot(portalSound); 
        }
    }
}
