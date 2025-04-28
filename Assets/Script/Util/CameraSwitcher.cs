using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCameraBase gameCamera;
    public CinemachineVirtualCameraBase inspectionCamera;
    public CinemachineVirtualCameraBase activeCamera;
    public Action onSwitchCamera;
    public CursorManager cursorManager;


    private InputAction switchBackAction;
    private bool allowCameraSwitchBack = false;


    private void Awake()
    {
        switchBackAction = new InputAction(binding: "<Keyboard>/space");
        switchBackAction.performed += _ => {
            if (allowCameraSwitchBack) SwitchToGameCamera();
        };
    }

    public void EnableCameraSwitchBack() => allowCameraSwitchBack = true;

    private void OnEnable() => switchBackAction.Enable();
    private void OnDisable() => switchBackAction.Disable();

    private void Start()
    {
        if (gameCamera != null && activeCamera == null)
        {
            activeCamera = gameCamera;
            gameCamera.Priority = 10;
            inspectionCamera.Priority = 0;
        }
    }

    public void SwitchToCamera(CinemachineVirtualCameraBase virtualCamera)
    {
        if (virtualCamera == null) return;

        if (activeCamera != null)
            activeCamera.Priority = 0;

        virtualCamera.Priority = 10;
        activeCamera = virtualCamera;
        onSwitchCamera?.Invoke();
    }

    public void SwitchToGameCamera()
    {
        Debug.Log("Switching to Game Camera!");
        SwitchToCamera(gameCamera);
        cursorManager.HideCursor();

    }

    public void SwitchToInspectionCamera()
    {
        Debug.Log("Switching to Inspection Camera!");
        SwitchToCamera(inspectionCamera);
    }

    public CinemachineVirtualCameraBase GetActiveCamera() => activeCamera;
}
