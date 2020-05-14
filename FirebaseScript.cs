﻿
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


public class FirebaseScript : LeadersBoard

{
    public Dictionary<string, List<string>> retriveList;

    public Dictionary<string, int> scoreList;
    public Dictionary<string, int> rankList;


    DatabaseReference reference;

    public string s;
    Firebase.Auth.FirebaseAuth auth;
    string name;
    Dictionary<string , string> names;

    




    // Start is called before the first frame update

    public void Start()

    {


        FirebaseDatabase.DefaultInstance.GetReference("names").LimitToFirst(5).ValueChanged += HandleNameValueChanged;


        names = new Dictionary<string, string>();
        LeadersBoardConstructor();
        retriveList = new Dictionary<string, List<string>>();
        Debug.Log("firebase script");
        scoreList = new Dictionary<string, int>();
        rankList = new Dictionary<string, int>();

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        if (auth.CurrentUser != null)
        {
            name = auth.CurrentUser.DisplayName;
        }
        else {
            name = "Auth Error";
        }

        setupFirebase();
        retriveData("Questions/");

        //  saveData("A", "B");

        //  saveData("A1", 21);
        saveData("ui", false);
        saveData("uid", 0);
        

        reference.ChildChanged += HandleChildAdded;

        reference.ChildChanged += HandleChildChanged;

        reference.ChildRemoved += HandleChildRemoved;

        reference.ChildMoved += HandleChildMoved;



        FirebaseDatabase.DefaultInstance.GetReference("currentPlayers").OrderByChild("score").LimitToLast(10).ValueChanged += HandleValueChanged;
        FirebaseDatabase.DefaultInstance.GetReference("rank/holders").OrderByChild("position").LimitToFirst(5).ValueChanged += HandleRankValueChanged;





    }



    public void setupFirebase()
    {

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://csi-treasurehunt.firebaseio.com/");

        reference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    // .GetReference("csi-treasurehunt/csi-treasurehunt/Questions/github/")

    public void retriveData(string reference)
    {
        Debug.Log("In retrive");


        FirebaseDatabase.DefaultInstance.RootReference.Child(reference).GetValueAsync().ContinueWith(task => {

            if (task.IsFaulted)

            {
                Debug.Log("faulted retrive");

                // Handle the error...

            }

            else if (task.IsCompleted)

            {



                DataSnapshot snapshot = task.Result;

                // string sss = (string)snapshot.Value;



                foreach (var childSnapshot in snapshot.Children)
                {

                    //  Debug.Log(childSnapshot.Key + " : " + childSnapshot.Value);
                    Debug.Log("QA: " + childSnapshot.Child("Q").Value + " : " + childSnapshot.Child("A").Value);
                    string nam = "Null";
                    names.TryGetValue(childSnapshot.Key.ToString(), out nam);
                    retriveList.Add(nam, new List<string> { childSnapshot.Child("Q").Value.ToString(), childSnapshot.Child("A").Value.ToString() });


                }

                //   Debug.Log("list QA : " + retriveList["github"][0]);



                // Do something with snapshot...

            }

        });



    }



    void HandleValueChanged(object sender, ValueChangedEventArgs args)

    {

        Debug.Log("in value change");

        if (args.DatabaseError != null)

        {

            Debug.LogError(args.DatabaseError.Message);

            return;

        }
        clear();
        scoreList.Clear();
        // Debug.Log(args.Snapshot.Child("1uid").Value);
        int i = 0;
        foreach (var childSnapshot in args.Snapshot.Children)
        {
             scoreList.Add(childSnapshot.Key.ToString(), int.Parse(childSnapshot.Child("score").Value.ToString()));
            
        }

      foreach (KeyValuePair<string, int> author in scoreList.OrderByDescending(key => key.Value))
        {

            string nam = "Null";
            names.TryGetValue(author.Key.ToString(), out nam);

            setTextBoard(i++, nam, author.Value.ToString());
           // scoreList.Add(author.Key.ToString(), int.Parse(author.Value.ToString()));
            Debug.Log(author.Key + " , " + author.Value);
        }
        //call function to update values in leaderboard UI using scoreList (showing live scores of all players)

    }



    void HandleNameValueChanged(object sender, ValueChangedEventArgs args)

    {
        Debug.Log("in value change");

        if (args.DatabaseError != null)

        {

            Debug.LogError(args.DatabaseError.Message);

            return;

        }

        names.Clear();
        // Debug.Log(args.Snapshot.Child("1uid").Value);
        foreach (var childSnapshot in args.Snapshot.Children)
        {
            Debug.Log(childSnapshot.Key + " : " + childSnapshot.Value);
            names.Add(childSnapshot.Key.ToString() , childSnapshot.Value.ToString());

        }

        //call function to update winners (rank) in leaderboard UI using rankList

    }




    void HandleRankValueChanged(object sender, ValueChangedEventArgs args)

    {
        Debug.Log("in value change");

        if (args.DatabaseError != null)

        {

            Debug.LogError(args.DatabaseError.Message);

            return;

        }
        rankList.Clear();
        int i = 0;
        // Debug.Log(args.Snapshot.Child("1uid").Value);
        foreach (var childSnapshot in args.Snapshot.Children)
        {
            Debug.Log(childSnapshot.Key + " : " + childSnapshot.Child("position").Value);
            string nam = "Null";
            names.TryGetValue(childSnapshot.Key, out nam);

            rankList.Add(nam, int.Parse(childSnapshot.Child("position").Value.ToString()));
            i++;

        }

        //call function to update winners (rank) in leaderboard UI using rankList

    }



    public void saveData(string child, int value)
    {

        Debug.Log(child + ", save data , " + value);
        
         reference.Child("currentPlayers").Child(child).Child("score").SetValueAsync(value);

    }



    public void saveData(string child, string value)
    {

        reference.Child("csi-treasurehunt").Child(child).SetValueAsync(value);

    }

    public void saveData(string child, bool value)
    {

        reference.Child("Players").Child(child).Child("canPlay").SetValueAsync(value);

    }





    void HandleChildAdded(object sender, ChildChangedEventArgs args)

    {

        if (args.DatabaseError != null)

        {

            Debug.LogError(args.DatabaseError.Message);

            return;

        }

        // Do something with the data in args.Snapshot

    }





    void HandleChildRemoved(object sender, ChildChangedEventArgs args)

    {

        if (args.DatabaseError != null)

        {

            Debug.LogError(args.DatabaseError.Message);

            return;

        }

        // Do something with the data in args.Snapshot

    }



    void HandleChildMoved(object sender, ChildChangedEventArgs args)

    {

        if (args.DatabaseError != null)

        {

            Debug.LogError(args.DatabaseError.Message);

            return;

        }

        // Do something with the data in args.Snapshot

    }



    void HandleChildChanged(object sender, ChildChangedEventArgs args)

    {

        if (args.DatabaseError != null)

        {

            Debug.LogError(args.DatabaseError.Message);

            return;

        }

        Debug.Log(args.Snapshot);



        // Rtext.text = args.Snapshot.Key;

        // Do something with the data in args.Snapshot

    }



    public int getCount()
    {
        Debug.Log("In get count");
        int a = -1;

        FirebaseDatabase.DefaultInstance.GetReference("rank/count").GetValueAsync().ContinueWith(task => {

            if (task.IsFaulted)

            {
                Debug.Log("faulted retrive rank");

                // Handle the error...

            }

            else if (task.IsCompleted)

            {



                DataSnapshot snapshot = task.Result;

                // string sss = (string)snapshot.Value;

                Debug.Log("rank value: " + snapshot.Value);
                a = int.Parse(snapshot.Value.ToString());



            }

        });


        return a;
    }

   public void saveRankCount(string child)

    {

      

        //  reference.Child("rank/count").SetValueAsync(value);

        Debug.Log("in save rank count function");

        FirebaseDatabase.DefaultInstance.GetReference("rank/count").RunTransaction(mutableData =>

        {

            Debug.Log("inside run transaction ");



            var c = mutableData.Value;

            Debug.Log(c);

            if (c == null)

            {

                Debug.Log("null count transaction");

            }

            else

            {

                int countVal = int.Parse(c.ToString());

                Debug.Log("count tran value : " + c);

                mutableData.Value = countVal + 1;

                Debug.Log("count tran value c+1 : " + (countVal + 1));

                saveRankerPosition(child, (countVal + 1));

            }

            return TransactionResult.Success(mutableData);



        });

    }
    
    
    public void DeleteFromPlayer(string child)

    {



        Debug.Log("Delete " + child);

        reference.Child("currentPlayers").Child(child).SetValueAsync(null);



    }

    public void saveRankerPosition(string child, int value)
    {

        //Debug.Log(child + ", save data , " + value);
        reference.Child("rank/holders").Child(child).Child("position").SetValueAsync(value);

    }

    // Update is called once per frame

    void Update()

    {



    }

}
