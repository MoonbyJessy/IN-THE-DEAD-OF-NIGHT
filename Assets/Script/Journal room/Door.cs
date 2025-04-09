using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    [SerializeField]
    private bool IsSlideDoor = true;
    [SerializeField]
    private Vector3 SlideDirection = Vector3.back;
    [SerializeField]
    private float slideAmount = 1.1f;
    [SerializeField]
    private float Speed = 1f;

    private Vector3 StartPosition;
    private Coroutine AnimationCoroutine;

    public Animator door;
    public GameObject openText;
    public AudioSource doorSound;

    public bool inReach;

    private void Start()
    {
        inReach = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }
    private void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            DoorOpens();
        }
        else
        {
            DoorCloses();
        }
    }
    void DoorOpens ()
    {
        Debug.Log("It Opens");
        door.SetBool("Open", true);
        door.SetBool("Closed", false);
        doorSound.Play();
    }
    void DoorCloses()
    {
        Debug.Log("Its Closes");
        door.SetBool("Open", false);
        door.SetBool("Closed", true);
    }
    private void Awake()
    {
        StartPosition = transform.position;
    }

    public void Open()
    {
        if (!IsOpen)
        {


            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            else
            {
                AnimationCoroutine = StartCoroutine(DoSlideOpen());

            }
        }
    }
    
    private IEnumerator DoSlideOpen()
    {
        Vector3 endPosition = StartPosition + slideAmount * SlideDirection;
        Vector3 startPosition = transform.position;

        float time = 0;
        IsOpen = true;
        while(time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);

            }
            if (IsSlideDoor)
            {
                AnimationCoroutine = StartCoroutine(DoSlideClose());

            }
        }
    }
    private IEnumerator DoSlideClose()
    {
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = transform.position;
        float time = 0;

        IsOpen = false;

        while(time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;

        }
    }
    public void Interact()
    {
        if (!IsOpen)
        {
            Open();
            // Switch camera after opening the door
            CameraSwitch cameraSwitch = FindObjectOfType<CameraSwitch>();
            if (cameraSwitch != null)
            {
                cameraSwitch.ManagerCamera();  // Switch to Camera 2
            }
        }
        else
        {
            Close();
            // Switch camera back after closing the door
            CameraSwitch cameraSwitch = FindObjectOfType<CameraSwitch>();
            if (cameraSwitch != null)
            {
                cameraSwitch.ManagerCamera();  // Switch to Camera 1
            }
        }
    }

    internal void Open(Vector3 position)
    {
        throw new NotImplementedException();
    }
}
