using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTimer : MonoBehaviour
{

    private UnityEngine.UI.Text text;
    public float timeShown;
    public Tag Text;

    void ShowMessage(string message, float timeToShow = 10)
    {
        timeShown += Time.deltaTime;
        if (timeShown >= timeToShow + 5)
            text.text = "";
        StartCoroutine(ShowMessageCoroutine(message, timeToShow));
        
    }

    IEnumerator ShowMessageCoroutine(string message, float timeToShow = 10)
    {
        while (timeShown < timeToShow)
        {
            timeShown += Time.deltaTime;
            yield return null;
        }

        text.text = " The Reqiure Score is 1700";
    }
}
