using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;



public class FirebaseScript : MonoBehaviour
{
    public Dictionary<string, List<string>> retriveList;



    DatabaseReference reference;

    public string s;





    // Start is called before the first frame update

    public void Start()

    {
        retriveList = new Dictionary<string, List<string>>();
        Debug.Log("firebase script");




        setupFirebase();
        retriveData("csi-treasurehunt/Questions/");
        saveData("A", "B");

        saveData("A1", 21);



        reference.ChildChanged += HandleChildAdded;

        reference.ChildChanged += HandleChildChanged;

        reference.ChildRemoved += HandleChildRemoved;

        reference.ChildMoved += HandleChildMoved;





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

    /*

        void HandleValueChanged(object sender, ValueChangedEventArgs args)

        {

            if (args.DatabaseError != null)

            {

                Debug.LogError(args.DatabaseError.Message);

                return;

            }

            Debug.Log(args.Snapshot.Value);

            Rtext.text = (string)args.Snapshot.Value;

            // Do something with the data in args.Snapshot

        }

        */



    public void saveData(string child, int value)
    {

        reference.Child("csi-treasurehunt").Child(child).SetValueAsync(value);

    }



    public void saveData(string child, string value)
    {

        reference.Child("csi-treasurehunt").Child(child).SetValueAsync(value);

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





    // Update is called once per frame

    void Update()

    {



    }

}
