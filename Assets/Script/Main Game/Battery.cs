using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] int batteryWeight;
    [SerializeField] KeyCode CollectKey = KeyCode.E;

    [SerializeField] GameObject[] HoverObject;

    public void OnMouseOver()
    {
        foreach (GameObject obj in HoverObject) obj.SetActive(true);
        if (Input.GetKeyDown(CollectKey))
        {
            FindAnyObjectByType<FlashlightManager>().GainBattery(batteryWeight);
            Destroy(this.gameObject);
        }
    }
    public void OnMouseExit()
    {
        foreach (GameObject obj in HoverObject) obj.SetActive(false);

    }
}
