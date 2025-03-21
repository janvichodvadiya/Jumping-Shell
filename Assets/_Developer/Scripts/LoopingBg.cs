using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBg : MonoBehaviour
{
    public float BackSpeed;
    public Renderer Render;
    void Start()
    {
        
    }
    void Update()
    {
        Render.material.mainTextureOffset += new Vector2(BackSpeed * Time.deltaTime, 0f);
    }
}
