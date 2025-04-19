using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Crashlytics;
using Firebase.Extensions;
using Firebase.Database;
using UnityEngine;
using UnityEngine.Diagnostics;
using System;

public class FirebaseManager : MonoBehaviour
{
    FirebaseAuth Auth;
    public DatabaseReference Database;

    public TMPro.TMP_InputField Email, Password;
    public string UserID;

    public UserData UserDataClss;

    private void Awake()
    {
        InitializeFireBase();
    }

    void InitializeFireBase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var result = task.Result;

            if (result == DependencyStatus.Available)
            {
                Debug.Log("Firebase Initialized");
                FirebaseAnalytics.LogEvent("Event log", "anything", 2512.15212);
                FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);

                Auth = FirebaseAuth.DefaultInstance;
                Database = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                Invoke(nameof(InitializeFireBase), 2);
            }
        });
    }

    public void CreateFirebaseUser()
    {
        Auth.CreateUserWithEmailAndPasswordAsync(Email.text, Password.text).ContinueWith(task =>
        {

            var user = task.Result;
            Debug.Log("Account created ");
            UserID = user.User.UserId;
            PlayerDataPrefs.IsLoggedIn = true;
            PlayerDataPrefs.Email = Email.text;
            PlayerDataPrefs.Password = Password.text;

        });
    }

    public void CrashGame()
    {
        throw new System.Exception("GameCrash");
    }

    public void LoginIntoFirebase()
    {
        string password = Password.text;
        string email = Email.text;
        Debug.Log("Username" + email + "::" + password);
        Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            Debug.Log("Git it here");
            if (task.IsFaulted) { Debug.Log(task.Exception.ToString()); return; }
            var user = task.Result;
            Debug.Log("Account created ");
            UserID = user.User.UserId;
            AddDatabaseData();
            PlayerDataPrefs.IsLoggedIn = true;
            PlayerDataPrefs.Email = email;
            PlayerDataPrefs.Password = password;
        });
    }

    public void AddDatabaseData()
    {
        UserDataClss.UserID = UserID;
        string json = JsonUtility.ToJson(UserDataClss);
        Database.Child("Users").Child(UserID).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted) { Debug.Log("DataSaved success fully"); }
        });
    }

    public void LoadDbData()
    {
        Database.Child("Users").Child(UserID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot data = task.Result;

                if (data != null)
                {
                    Debug.Log(data.ChildrenCount);
                    Debug.Log(data.Child("UserName").Value);
                }
            }
        });
    }
}

[System.Serializable]

public class UserData
{
    public string UserID;
    public string UserName;
    public int Score;
}
