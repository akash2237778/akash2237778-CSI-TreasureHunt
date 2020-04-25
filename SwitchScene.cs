using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    // Start is called before the first frame update
 
    public static void changeScene() {
        Debug.Log("Scene Loading.......");
        SceneManager.LoadScene("GameScene");
    }

    void Start() { }
    void Update() { }
    // Update is called once per frame

}
