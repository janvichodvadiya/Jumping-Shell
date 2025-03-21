using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update()
    {
        if (player != null)
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -10f);
    }
}
