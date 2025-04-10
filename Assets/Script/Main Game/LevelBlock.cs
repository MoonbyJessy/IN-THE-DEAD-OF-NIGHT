using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    public GameObject bloackText;

    void OnTriggerEnter(Collider other)
    {
        bloackText.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        bloackText.SetActive(false);
    }
}
