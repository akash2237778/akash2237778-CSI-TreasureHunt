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

    // Start is called before the first frame update
    private void Awake()
    {
        entryContainer = Container.transform;
        entryTemplate = entryContainer.Find("Template");

        entryTemplate.gameObject.SetActive(false);

        float height = 20f;
        for (int i = 0; i < 10; i++) {
            Transform entryTransform = Instantiate( entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -height * i);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("Rank").GetComponent<Text>().text = "2";
            entryTransform.Find("Name").GetComponent<Text>().text = "AAk" + i;
            entryTransform.Find("Score").GetComponent<Text>().text = "R:" + i;
        }
    }

    // Update is called once per frame
   
}
