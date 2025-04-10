using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQTE : MonoBehaviour
{
    public GameObject qteObject; //enemy
    public static int timesDone;

    private void OnTriggerEnter(Collider other)
    {
        qteObject.SetActive(true);
        GetComponent<BoxCollider>().enabled = false;

    }
   
    // Update is called once per frame
    void Update()
    {
        if (timesDone == 3)
        {
            qteObject.SetActive (false);
        }
    }
}
