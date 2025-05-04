using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool inReach;

    public GameObject player;

    private GameObject Flashlight;
    public float maxpickupdistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        inReach = false;
        Flashlight = GameObject.Find("Flashlight");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            Flashlight.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.C))
        {
            Pickup();
            Flashlight.GetComponent<Flashlight>().batteries += 1;
        }


    }
    public void Pickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, maxpickupdistance))
        {
            if (hit.transform.CompareTag("PickUp"))
            {

                Destroy(hit.transform.gameObject);
            }


        }
    }
}
