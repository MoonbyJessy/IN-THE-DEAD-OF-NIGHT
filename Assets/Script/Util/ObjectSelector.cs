using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectSelector : MonoBehaviour
{
    public Camera mainCamera;
    public CameraSwitcher cameraSwitcher;
    public float zoomMultiplier = 2;
    public float zoomInSpeed = 1;
    public float zoomOutSpeed = 10;
    private Item currentItem;
    public float cooldownTime = 1;
    private float lastFoundClueTime;
    [SerializeField]private Vector3 initialPosition = Vector3.zero;
    [SerializeField] private Vector3 targetPosition = Vector3.zero;
    private float zoomSpeed;
    //private Quaternion initialRotation;

    private void Start()
    {
        cameraSwitcher.onSwitchCamera += SaveInitialPosition;
        SaveInitialPosition();
    }

    private void SaveInitialPosition()
    {
        Debug.Log("Switched camera");
        initialPosition = cameraSwitcher.GetActiveCamera().transform.position;
        targetPosition = initialPosition;
    }

    private void LateUpdate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        CinemachineVirtualCameraBase activeCamera = cameraSwitcher.GetActiveCamera();
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            if (hit.collider.TryGetComponent(out Item item))
            {
                Debug.Log("Hovering over " + item.gameObject.name);
                if (currentItem == null)
                {
                   /* if (!item.IsFound() && Time.time > cooldownTime + lastFoundClueTime)
                    {*/
                        currentItem = item;
                        currentItem.ShowPopUp();
                    //}
                }
            }
            else
            {
                if (currentItem != null)
                {
                    currentItem = null;
                    lastFoundClueTime = Time.time;
                }
            }
        }
        else
        {
            if (currentItem != null)
            {
                currentItem = null;
                lastFoundClueTime = Time.time;
            }
        }

        if (currentItem != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Pressed on item: " +  currentItem.gameObject.name);
            }
            float distance = Vector3.Distance(currentItem.transform.position, initialPosition);
            Vector3 zoomDirection = (currentItem.transform.position-initialPosition).normalized;
            targetPosition = initialPosition + zoomDirection * (distance / zoomMultiplier);
            zoomSpeed = zoomInSpeed;

        }
        else
        {
            targetPosition = initialPosition;
            zoomSpeed = zoomOutSpeed;
        }
        activeCamera.transform.position = Vector3.Lerp(activeCamera.transform.position, targetPosition, Time.deltaTime * zoomSpeed);
    }

}
