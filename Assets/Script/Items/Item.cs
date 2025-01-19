using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public CanvasGroup cluePopUp;
    private bool isFound;

    private void Start()
    {
        cluePopUp.alpha = 0;
    }
    public void ShowPopUp()
    {
        if (isFound) return;
        StartCoroutine(ShowPopUpCoroutine());
        isFound = true;
    }

    private IEnumerator ShowPopUpCoroutine()
    {
        float t = 0;
        while (t < 1)
        {
            cluePopUp.alpha = t;
            t += Time.deltaTime;
            yield return null;
        }
        cluePopUp.alpha = 1;
    }

    public bool IsFound()
    {
        return isFound;
    }
}
