using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FirebaseScript : MonoBehaviour
{
    public TextMesh Rtext;
    
    // Start is called before the first frame update
    void Start()
    {
        Rtext.text = "Hello";
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://csi-treasurehunt.firebaseio.com/");

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        //send
        reference.Child("csi-treasurehunt").Child("Child").SetValueAsync("val123ue");

        //retrive
       /* FirebaseDatabase.DefaultInstance
  .GetReference("csi-treasurehunt")
  .GetValueAsync().ContinueWith(task => {
      if (task.IsFaulted)
      {
          Rtext.text = "True";
          // Handle the error...
      }
      else if (task.IsCompleted)
      {

          DataSnapshot snapshot = task.Result;
          Rtext.text = snapshot.Key;
          Debug.Log(snapshot);
          }
  });*/
       

        //var ref = FirebaseDatabase.DefaultInstance.GetReference("GameSessionComments");
        FirebaseDatabase.DefaultInstance.GetReference("csi-treasurehunt").ChildChanged += HandleChildChanged;
        //ref.ChildChanged += HandleChildChanged;
        //ref.ChildRemoved += HandleChildRemoved;
        //ref.ChildMoved += HandleChildMoved;

    }



    void HandleChildChanged(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Debug.Log(args.Snapshot);
        
        Rtext.text = args.Snapshot.Key;
        // Do something with the data in args.Snapshot
    }

   

   





    // Update is called once per frame
    void Update()
    {
        
    }
}
