using UnityEngine;

public class FrameCollect : MonoBehaviour
{
public class FrameCollection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("SmallPlayer"))
        {
            Debug.Log("Frame Collected!"); // Debug Check
            collision.GetComponent<Prefab>().IncreaseSize();  // Increase Player Size
            gameObject.SetActive(false);  // Remove Frame
        }
    }
}

}
