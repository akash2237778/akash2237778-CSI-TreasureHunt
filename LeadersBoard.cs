using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        }
    }

    // Update is called once per frame
   
}
