using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    public GameObject frame04; // 0.4 size frame
    public GameObject frame05; // 0.5 size frame

    private bool smallFrameCollected = false; // Track if 0.1 frame is collected

    void Start()
    {
        // Disable collision of larger frames, but keep them visible
        frame04.GetComponent<Collider2D>().enabled = false;
        frame05.GetComponent<Collider2D>().enabled = false;
    }

    public void UnlockBiggerFrames()
    {
        smallFrameCollected = true;

        // Enable collision so they can be collected
        frame04.GetComponent<Collider2D>().enabled = true;
        frame05.GetComponent<Collider2D>().enabled = true;
    }

    public bool CanCollectFrame(float frameSize)
    {
        // Allow 0.1 size frame always
        if (frameSize == 0.1f) return true;

        // Allow 0.4 & 0.5 only after 0.1 is collected
        return smallFrameCollected && (frameSize == 0.4f || frameSize == 0.5f);
    }
}
