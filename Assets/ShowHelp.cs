using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHelp : MonoBehaviour
{
    // Start is called before the first frame update
    public Button help;
    public GameObject helpPanel;
    int c = 0;
    void Start()
    {
        Debug.Log("Hi");
        //help = GameObject.Find("helpButton").GetComponent<Button>();
       // helpText = GameObject.Find("helpText").GetComponent<Text>();
        help.onClick.AddListener(Help);
    }

    // Update is called once per frame
    void Update()
    {
        if (c % 2 == 0)
        {
            helpPanel.SetActive(false);
            help.GetComponentInChildren<Text>().text = "Show Help";
        }
        else
        {
            helpPanel.SetActive(true);
            help.GetComponentInChildren<Text>().text = "Hide Help";
        }
    }
    
    void Help()
    {
        Debug.Log("Updating");
        c++;
    }
}
