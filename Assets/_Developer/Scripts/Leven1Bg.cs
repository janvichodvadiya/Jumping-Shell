using UnityEngine;

public class Leven1Bg : MonoBehaviour
{
    public float loop;
    public Renderer Render;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Render.material.mainTextureOffset += new Vector2(loop * Time.deltaTime,0f);
    }
}
