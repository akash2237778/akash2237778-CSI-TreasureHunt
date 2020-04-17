using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public InputField phno;
    public InputField otp;
    string phoneNumber;
    PhoneAuthProvider provider;
    string Id;
    FirebaseAuth firebaseAuth;
    public Button codeGenerate;
    public Button SignIn;
    FirebaseUser User;
    string UserId;
    string _phoneNumber;
    private const uint PhoneAuthTimeout = 120000;
    string _verificationId;
    private static float _elapsedTime;
    public Text debug;


    public void OnClickGenerateOtp() {
        

        phoneNumber = phno.text;
        Debug.Log(codeGenerate.onClick + "  ");
        Debug.Log(phoneNumber + "  ");
        firebaseAuth = FirebaseAuth.DefaultInstance;
        firebaseAuth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

        VerifyPhoneNumber(phoneNumber);
        
    }

    private void AuthStateChanged(object sender, EventArgs eventArgs)
    {
        Debug.Log("Auth State Changed");

        if (firebaseAuth.CurrentUser != User)
        {
            var signedIn = User != firebaseAuth.CurrentUser && firebaseAuth.CurrentUser != null;

            if (!signedIn && User != null)
            {
                Debug.Log("Signed out " + User.UserId);
            }

            User = firebaseAuth.CurrentUser;
            if (User != null)
            {
                UserId = User.UserId;

                if (signedIn)
                {
                    Debug.Log("Signed in " + User.UserId);
                }
            }
        }
        else
        {
            Debug.Log("Sign-in not done");
        }
    }

    public void VerifyPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;

        provider = PhoneAuthProvider.GetInstance(firebaseAuth);
        provider.VerifyPhoneNumber(_phoneNumber, PhoneAuthTimeout, null,
            verificationCompleted: (credential) =>
            {
                Debug.Log("verification completed");
            },
            verificationFailed: (error) =>
            {
                Debug.Log("verification failed");
            },
            codeSent: (id, token) =>
            {
                Debug.Log("code sent");

                _verificationId = id;
                Debug.Log(" vericationID " + _verificationId );
            },
            codeAutoRetrievalTimeOut: (id) =>
            {
                Debug.Log("code auto retrieval timed Out");
            });

      

    }



/*
    public void VerifyCode(string code)
    {
        StartCoroutine(VerifyCodeAsync(code));
    }


    private IEnumerator VerifyCodeAsync(string code)
    {
       
        var credential = provider.GetCredential(_verificationId, code);

        var task = firebaseAuth.SignInWithCredentialAsync(credential);
        yield return new WaitWhile(() => IsTask(task.IsCompleted));

        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.Log("faulted");
            yield return null;
        }
        else
        {
            Debug.Log("Not faulted");

        }
    }

    private static bool IsTask(bool value)
    {
        _elapsedTime += Time.deltaTime;

        if (value)
        {
            return false;
        }
        else
        {
            if (_elapsedTime > 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    */


    public void OnClickGoogleSignIn() {
        Debug.Log("ID" + _verificationId);
        Credential credential = provider.GetCredential(_verificationId, otp.text );
        firebaseAuth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " +
                               task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.Log("User signed in successfully");
            // This should display the phone number.
            Debug.Log("Phone number: " + newUser.PhoneNumber);
            // The phone number providerID is 'phone'.
            Debug.Log("Phone provider ID: " + newUser.ProviderId);
        });

    }

}
