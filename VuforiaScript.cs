using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using Vuforia;







public class VuforiaScript : UIHandler, IObjectRecoEventHandler
{

    private CloudRecoBehaviour mCloudRecoBehaviour;

    private bool mIsScanning = false;

    private string mTargetMetadata = "";

    public ImageTargetBehaviour ImageTargetTemplate;

    public TextMesh RText;

    public static bool isfound = false;

    FirebaseScript obj;
      GameObject g;

    string prev_ans;

    private string uid = "uid1";
    private int score = 0;

    // Use this for initialization 

    void Start()

    {
        Debug.Log(UserId);
        g = Camera.main.gameObject;
        obj = g.GetComponent<FirebaseScript>();

        uid = UserId;

        prev_ans = "github";
      //  obj.saveData(uid, false);
       // obj.saveData(uid, 0);
        // register this event handler at the cloud reco behaviour 

        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();

        if (mCloudRecoBehaviour)
        {
            mCloudRecoBehaviour.RegisterEventHandler(this);
        }
    }

    void check(string imgScanned)
    {
        if (imgScanned.Equals(obj.retriveList[prev_ans][1]))
        {
            Debug.Log("correct answer");

            score++;

            obj.saveData(uid, score);
            prev_ans = imgScanned;
            Debug.Log("prev_ans updated: " + imgScanned);
            //call function to display next question 
            if (score == obj.retriveList.Count)
            {
                 // int c=obj.getCount();

                //c++;

                obj.saveRankCount(uid);

                //   obj.saveRankerPosition(uid, c);

                obj.DeleteFromPlayer(uid);



                //Finish game
            }

            //call function to update user's displayed score (UI)

        }
        else
        {
            Debug.Log("display scan again message (wrong answer)");
        }
    }

    public void OnInitialized(TargetFinder targetFinder)

    {

        Debug.Log("Cloud Reco initialized");

    }

    public void OnInitError(TargetFinder.InitState initError)

    {

        Debug.Log("Cloud Reco init error " + initError.ToString());

    }

    public void OnUpdateError(TargetFinder.UpdateState updateError)

    {

        Debug.Log("Cloud Reco update error " + updateError.ToString());

    }



    public void OnStateChanged(bool scanning)

    {

        mIsScanning = scanning;

        if (scanning)

        {

            // clear all known trackables

            var tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

            tracker.GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);

        }

    }



    // Here we handle a cloud target recognition event

    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)

    {

        TargetFinder.CloudRecoSearchResult cloudRecoSearchResult =

            (TargetFinder.CloudRecoSearchResult)targetSearchResult;

        // do something with the target metadata

        mTargetMetadata = cloudRecoSearchResult.MetaData;

        // stop the target finder (i.e. stop scanning the cloud)

        mCloudRecoBehaviour.CloudRecoEnabled = false;





        if (ImageTargetTemplate)

        {

            isfound = true;

            // enable the new result with the same ImageTargetBehaviour: 

            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

            tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, ImageTargetTemplate.gameObject);






            RText.text = obj.retriveList[targetSearchResult.TargetName][0];
            Debug.Log("s c h : " + RText.text);

            check(targetSearchResult.TargetName);



        }

    }



    void OnGUI()

    {

        // Display current 'scanning' status

        GUI.Box(new Rect(100, 100, 200, 50), mIsScanning ? "Scanning" : "Not scanning");

        // Display metadata of latest detected cloud-target

        GUI.Box(new Rect(100, 200, 200, 50), "Metadata: " + mTargetMetadata);

        // If not scanning, show button

        // so that user can restart cloud scanning

        if (!mIsScanning)

        {

            if (GUI.Button(new Rect(100, 300, 200, 50), "Restart Scanning"))

            {

                // Restart TargetFinder

                mCloudRecoBehaviour.CloudRecoEnabled = true;

            }

        }

    }





    // Update is called once per frame

    void Update()

    {



    }

}
