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
    public GameObject gotBagBox;


    
    void Update()
    {
        if (waiting == true)
        {
            waiting = false;
            QTEGen = Random.Range(1, 3);
            if (QTEGen == 1 ) 
            {
                displayBox.GetComponent<Text>().text = "[1]";
            
            }
            if (QTEGen == 2)
            {
                displayBox.GetComponent<Text>().text = "[3]";

            }
            
        }
        if (QTEGen == 1)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("1Key"))
                {
                    correctKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("3Key"))
                {
                    correctKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

    }

    IEnumerator KeyPressing()
    {
        QTEGen = 4;
        if (correctKey == 1)
        {
            gotBagBox.GetComponent<Text>().text = "Correct";
            yield return new WaitForSeconds(1.5f);
            correctKey = 0;
            gotBagBox.GetComponent<Text>().text = "";
            displayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);

            FirstQTE.timesDone += 1;
            waiting = true;

        }
        if (correctKey == 2)
        {
            gotBagBox.GetComponent<Text>().text = "Incorrect";
            yield return new WaitForSeconds(1.5f);
            correctKey = 0;
            gotBagBox.GetComponent<Text>().text = "";
            displayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);

            
            waiting = true;

        }
    }
}
