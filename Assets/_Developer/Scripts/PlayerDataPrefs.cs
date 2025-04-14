using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataPrefs
{
    public static string Email
    {
        get => PlayerPrefs.GetString("FirebaseDemo.Email", string.Empty);
        set => PlayerPrefs.SetString("FirebaseDemo.Email", value);
    }

    public static string Password
    {
        get => PlayerPrefs.GetString("FirebaseDemo.Password", string.Empty);
        set => PlayerPrefs.SetString("FirebaseDemo.Password", value);
    }

    public static bool IsLoggedIn
    {
        get => PlayerPrefs.GetInt("IsLoggedIn") == 1;
        set => PlayerPrefs.SetInt("IsLoggedIn", value ? 1 : 0);
    }
}
