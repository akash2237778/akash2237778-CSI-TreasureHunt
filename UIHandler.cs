using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    public InputField Email;
    public InputField Password;
    public Button SignIn;
    Firebase.Auth.FirebaseUser newUser;
    public Button chngScene;
    public Text signInText;
    int retInt = 0;

    void Start() {
        //SceneManager.LoadScene("GameScene");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Debug.Log(auth);
        SignIn.onClick.AddListener(delegate () { signIn(Email.text, Password.text); });
    }


    public int signIn(string email, string password) {
        signInText.text = "Start Game";
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                retInt = 0;
                return;
            }
            if (task.IsFaulted)
            {
                signInText.text = "Error Occured";
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                retInt = 1;
                return;
            }
            if (task.IsCompleted) {
                newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
                retInt = 2;
            }

            if (retInt < 2)
            {
                signInText.text = "Error Occured";
            }

        });

        if (retInt == 2)
        {
            SceneManager.LoadScene("GameScene");
        }

        return retInt;
    }


    //to Create User
    /*public void createUser(string email , string password) {
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
    }*/


    void Update() {

    }

}
