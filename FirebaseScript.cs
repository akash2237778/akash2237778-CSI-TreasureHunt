using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FirebaseScript : MonoBehaviour
{
  //  public TextMesh Rtext;
    DatabaseReference reference;
    public string s;
    

    // Start is called before the first frame update
    public void Start()
    {
        //Rtext.text = "Hello";

        setupFirebase();
        saveData("Akash", "Saini");
        saveData("Ak", 2);

        reference.ChildChanged += HandleChildAdded;
        reference.ChildChanged += HandleChildChanged;
        reference.ChildRemoved += HandleChildRemoved;
        reference.ChildMoved += HandleChildMoved;
       

    }

    public void setupFirebase() {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://csi-treasurehunt.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void retriveData(string reference) {
        
        FirebaseDatabase.DefaultInstance
          .GetReference(reference)
          .GetValueAsync().ContinueWith(task => {
              if (task.IsFaulted)
              {
              // Handle the error...
                }
              else if (task.IsCompleted)
              {
                 
                  DataSnapshot snapshot = task.Result;
                 // string sss = (string)snapshot.Value;

                  List<string> list = new List<string>();
                  foreach (var childSnapshot in snapshot.Children)
                  {
                      //Debug.Log(childSnapshot.Key);
                      list.Add(childSnapshot.Key);
                  }

                  s = list[0];
                 // Rtext.text = "s";
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

    public void saveData(string child, int value) {
        reference.Child("csi-treasurehunt").Child(child).SetValueAsync(value);
    }

    public void saveData(string child, string value) {
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
