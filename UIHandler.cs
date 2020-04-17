using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    public InputField Email;
    public InputField Password;
    public Button CreateUser;
    Firebase.Auth.FirebaseUser newUser;

    void Start() {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Debug.Log(auth);
        CreateUser.onClick.AddListener(delegate () { createUser(Email.text, Password.text); });

    }



    public void createUser(string email , string password) {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
       //throw new NotImplementedException();
    }


    void Update() { }

}
