using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{

    public GameObject displayBox;
    public int QTEGen;
    public bool waiting = true;
    public int correctKey;



    // Update is called once per frame
    void Update()
    {
        if (waiting == true)
        {
            waiting = false;
            QTEGen = Random.Range(1, 3);
            if (QTEGen == 1 ) 
            {
                displayBox.GetComponent<Text>().text = "[R]";
            
            }
            if (QTEGen == 2)
            {
                displayBox.GetComponent<Text>().text = "[T]";

            }
        }

    }
}
