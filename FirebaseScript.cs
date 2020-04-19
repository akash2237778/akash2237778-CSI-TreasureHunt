
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


/*
 * public class Qlist
{
   public Qlist(string k, string v) {
        key = k;
        value = v;
    }
    public string key;
    public string value;


}*/
public class FirebaseScript : MonoBehaviour

{
    public Dictionary<string, List<string>> retriveList;

    public Dictionary<string, int> scoreList;
    public Dictionary<string, int> rankList;


    DatabaseReference reference;

    public string s;





    // Start is called before the first frame update

    public void Start()

    {
        retriveList = new Dictionary<string, List<string>>();
        Debug.Log("firebase script");
        scoreList = new Dictionary<string, int>();
        rankList = new Dictionary<string, int>();



        setupFirebase();
        retriveData("Questions/");

        //  saveData("A", "B");

        //  saveData("A1", 21);



        reference.ChildChanged += HandleChildAdded;

        reference.ChildChanged += HandleChildChanged;

        reference.ChildRemoved += HandleChildRemoved;

        reference.ChildMoved += HandleChildMoved;

        FirebaseDatabase.DefaultInstance.GetReference("Players").OrderByChild("score").ValueChanged += HandleValueChanged;

        FirebaseDatabase.DefaultInstance.GetReference("rank/holders").OrderByChild("position").ValueChanged += HandleRankValueChanged;

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
                    retriveList.Add(childSnapshot.Key.ToString(), new List<string> { childSnapshot.Child("Q").Value.ToString(), childSnapshot.Child("A").Value.ToString() });


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
        scoreList.Clear();
        // Debug.Log(args.Snapshot.Child("1uid").Value);
        foreach (var childSnapshot in args.Snapshot.Children)
        {
            Debug.Log(childSnapshot.Child("name").Value + " : " + childSnapshot.Child("score").Value);
            scoreList.Add(childSnapshot.Child("name").Value.ToString(), int.Parse(childSnapshot.Child("score").Value.ToString()));
        }

        //call function to update values in leaderboard UI using scoreList (showing live scores of all players)

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
        // Debug.Log(args.Snapshot.Child("1uid").Value);
        foreach (var childSnapshot in args.Snapshot.Children)
        {
            Debug.Log(childSnapshot.Key + " : " + childSnapshot.Child("position").Value);
            rankList.Add(childSnapshot.Key.ToString(), int.Parse(childSnapshot.Child("position").Value.ToString()));


        }

        //call function to update winners (rank) in leaderboard UI using rankList

    }



    public void saveData(string child, int value)
    {

        Debug.Log(child + ", save data , " + value);
        reference.Child("Players").Child(child).Child("score").SetValueAsync(value);

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

    public void saveRankCount(int value)
    {

        //Debug.Log(child + ", save data , " + value);
        reference.Child("rank/count").SetValueAsync(value);

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
