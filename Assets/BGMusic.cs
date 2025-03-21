using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    private static BGMusic BgSound;
    private void Awake()
    {
        if(BgSound == null)
        {
            BgSound = this;
          //  DontDestroyOnLoad(BgSound);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
