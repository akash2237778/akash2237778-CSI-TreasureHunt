using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class Player : FirebaseScript
{
    string playerName;
    string email;
    int rank;
    DateTime startTime;
    Time endTime;
    int quesSolved = 0;
    Firebase.Auth.FirebaseUser user;
    string uid;
    int TotalQues = 10;

    Player(string name)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            email = user.Email;
            //System.Uri photo_url = user.PhotoUrl;
            uid = user.UserId;
        }
        playerName = name;
        saveData("csi-treasurehunt/Players/" + uid + "/Name/", name);
        getStartTime();
    }

    void getStartTime()
    {
        startTime = DateTime.Now;
        Debug.Log(startTime);
        saveData("csi-treasurehunt/Players/" + uid + "/StartTime/", startTime.Hour + ":"+ startTime.Minute + ":" + startTime.Second);
    }

    private void saveNumberOfQSol()
    {
        if (quesSolved == TotalQues) {
            //Game Finished
            saveData("csi-treasurehunt/Players/" + uid + "/EndTime/", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
        }
        saveData("csi-treasurehunt/Players/" + uid + "/Score/", quesSolved);
        saveData("csi-treasurehunt/Players/" + uid + "/scoreTime/" + quesSolved, DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
        quesSolved++;
    }


}
