using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeadersBoard : MonoBehaviour
{
    public GameObject Container;
   // public GameObject Template;
    private Transform entryContainer;
    private Transform entryTemplate;
    float height = 20f;
    List<Transform> entryTransformList;
    List<RectTransform> entryRectTransformList;

    // Start is called before the first frame update
    public void LeadersBoardConstructor()
    {
       Debug.Log(Container.transform);
        entryContainer = Container.transform;
        entryTemplate = entryContainer.Find("Template");

        entryTemplate.gameObject.SetActive(false);

        entryTransformList = new List<Transform>();

    }

    public void setTextBoard(int i , string name , string score) {
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -height * i);
        entryTransform.gameObject.SetActive(true);
        
        entryTransform.Find("Rank").GetComponent<Text>().text = i.ToString();
        entryTransform.Find("Name").GetComponent<Text>().text = name;
        entryTransform.Find("Score").GetComponent<Text>().text = score;
        Debug.Log(name +"Board");
        entryTransformList.Add(entryTransform);
       }

    public void clear() {
        Debug.Log("in CLear......");
        for (int i = 0; i < entryTransformList.Count; i++) {
            entryTransformList[i].gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
   
}
