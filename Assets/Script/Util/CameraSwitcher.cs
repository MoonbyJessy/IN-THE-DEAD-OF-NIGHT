using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCameraBase gameCamera;
    public CinemachineVirtualCameraBase activeCamera;
    public Action onSwitchCamera;
    private InputAction switchBackAction;

    private void OnEnable()
    {
        switchBackAction.Enable();
        switchBackAction.performed += _ => SwitchToGameCamera();
    }

    private void OnDisable()
    {
        switchBackAction.Disable();
    }
    private void Start()
    {
        if (switchBackAction == null)
        {
            switchBackAction = new InputAction(binding: "<Keyboard>/space");
        } 
    }
    public void SwitchToCamera(CinemachineVirtualCameraBase virtualCamera)
    {
        activeCamera.Priority = 0;
        virtualCamera.Priority = 1;
        activeCamera = virtualCamera;
        onSwitchCamera?.Invoke();
    }
    public void SwitchToGameCamera()
    {
        Debug.Log("switch!");
        SwitchToCamera(gameCamera);
    }

    public CinemachineVirtualCameraBase GetActiveCamera() { return activeCamera; }

}
