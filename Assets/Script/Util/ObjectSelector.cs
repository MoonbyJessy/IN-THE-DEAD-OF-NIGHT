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
    public float zoomMultiplier = 2f;
    public float zoomInSpeed = 1f;
    public float zoomOutSpeed = 10f;
    public float cooldownTime = 1f;
    public List<Item> allViewableItems;

    private Item currentItem;
    private float lastFoundClueTime;
    private Vector3 initialPosition = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;
    private float zoomSpeed;
    private bool isInInspectionMode = false;
    private Dictionary<Item, bool> itemHoverStates = new Dictionary<Item, bool>();
    private bool allItemsViewed = false;

    private void Start()
    {
        cameraSwitcher.onSwitchCamera += SaveInitialPosition;
        SaveInitialPosition();

        foreach (Item item in allViewableItems)
        {
            itemHoverStates.Add(item, false);
        }
    }

    private void Update()
    {
        if (allItemsViewed && Input.GetKeyDown(KeyCode.Space))
        {
            ReturnToPlayerCamera();
        }
    }

    private void LateUpdate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        CinemachineVirtualCameraBase activeCamera = cameraSwitcher.GetActiveCamera();

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.TryGetComponent(out Item item))
            {
                if (currentItem == null || currentItem != item)
                {
                    Debug.Log("Hovering over " + item.gameObject.name);
                    currentItem = item;
                    currentItem.ShowPopUp();
                    isInInspectionMode = true;

                    if (itemHoverStates.ContainsKey(currentItem))
                    {

                        if (!itemHoverStates[currentItem])
                        {
                            itemHoverStates[currentItem] = true;
                            CheckAllItemsViewed();
                        }
                    }
                }
            }
            else
            {
                ClearCurrentItem();
            }
        }
        else
        {
            ClearCurrentItem();
        }

        if (isInInspectionMode)
        {
            if (currentItem != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Pressed on item: " + currentItem.gameObject.name);
                }

                float distance = Vector3.Distance(currentItem.transform.position, initialPosition);
                Vector3 zoomDirection = (currentItem.transform.position - initialPosition).normalized;
                targetPosition = initialPosition + zoomDirection * (distance / zoomMultiplier);
                zoomSpeed = zoomInSpeed;
            }
            else
            {
                targetPosition = initialPosition;
                zoomSpeed = zoomOutSpeed;
            }

            activeCamera.transform.position = Vector3.Lerp(
                activeCamera.transform.position,
                targetPosition,
                Time.deltaTime * zoomSpeed
            );
        }
    }

    private void CheckAllItemsViewed()
    {
        allItemsViewed = true;
        foreach (var pair in itemHoverStates)
        {
            if (!pair.Value)
            {
                allItemsViewed = false;
                return;
            }
        }

        if (allItemsViewed)
        {
            Debug.Log("All items have been viewed! Press SPACE to return to player camera.");
            cameraSwitcher.EnableCameraSwitchBack();

        }
    }

    private void ReturnToPlayerCamera()
    {
        cameraSwitcher.SwitchToGameCamera();
        allItemsViewed = false;
        isInInspectionMode = false;
    }

    private void ClearCurrentItem()
    {
        if (currentItem != null)
        {
            currentItem = null;
            lastFoundClueTime = Time.time;
        }
    }

    private void SaveInitialPosition()
    {
        Debug.Log("Switched camera");
        initialPosition = cameraSwitcher.GetActiveCamera().transform.position;
        targetPosition = initialPosition;
    }

    public bool HasItemBeenHovered(Item item)
    {
        if (itemHoverStates.TryGetValue(item, out bool hasBeenHovered))
        {
            return hasBeenHovered;
        }
        return false;
    }

    public List<Item> GetHoveredItems()
    {
        List<Item> hoveredItems = new List<Item>();
        foreach (var pair in itemHoverStates)
        {
            if (pair.Value)
            {
                hoveredItems.Add(pair.Key);
            }
        }
        return hoveredItems;
    }

}
