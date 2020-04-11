using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Qlist
{
   public Qlist(string k, string v) {
        key = k;
        value = v;
    }
    public string key;
    public string value;


}

public class FirebaseScript : MonoBehaviour
{

    public List<Qlist> list;

    //  public TextMesh Rtext;

    DatabaseReference reference;

    public string s;





    // Start is called before the first frame update

    public void Start()

    {
        Debug.Log("firebase script");
        list = new List<Qlist>();
        //Rtext.text = "Hello";



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

                  Debug.Log(childSnapshot.Key+" : "+childSnapshot.Value);
                    list.Add(new Qlist(childSnapshot.Key.ToString(), childSnapshot.Value.ToString()));

                  //list.Add(childSnapshot.Child("Q").Value.ToString());

                }



              s = list[0].value;
             Debug.Log("retrived : "+s);
                for (int i = 0; i < list.Count; i++)
                {
                    Debug.Log("-----" + list[i].value);
                }

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

