using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundonOff : MonoBehaviour
{
    [SerializeField] Image SoundOn;
    [SerializeField] Image SoundOff;
    private bool muted = false;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateIcon();
        AudioListener.pause = muted;
    }

    private void UpdateIcon()
    {
        if (muted == false)
        {
            SoundOn.enabled = true;
            SoundOff.enabled = false;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
    }

    public void OnBtnPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateIcon();
    }

  
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted" , muted ? 1 : 0);
    }
}
