using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform mainCamera;
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.position);
    }

    private void Reset()
    {
        mainCamera = Camera.main.transform;
    }
}
