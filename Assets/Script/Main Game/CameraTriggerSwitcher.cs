using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
using System;

public class CameraTriggerSwitcher : MonoBehaviour
{
    [Header("Virtual Cameras")]
    public CinemachineVirtualCameraBase gameCamera;
    public CinemachineVirtualCameraBase sisterCamera;

    [Header("Mouse Exit")]
    [SerializeField] private InputAction exitAction; // Left click

    private CinemachineVirtualCameraBase activeCamera;
    public Action onSwitchCamera;

    private void OnEnable()
    {
        exitAction.Enable();
    }

    private void OnDisable()
    {
        exitAction.Disable();
    }

    private void Start()
    {
        // Set initial active camera
        activeCamera = gameCamera;
        gameCamera.Priority = 1;
        sisterCamera.Priority = 0;

        if (exitAction == null)
            exitAction = new InputAction(binding: "<Keyboard>/e");

        exitAction.performed += _ => ExitToGameCamera();
    }

    private void SwitchToCamera(CinemachineVirtualCameraBase targetCam)
    {
        activeCamera.Priority = 0;
        targetCam.Priority = 1;
        activeCamera = targetCam;
        onSwitchCamera?.Invoke();
    }

    public void SwitchToSisterCamera()
    {
        if (activeCamera != sisterCamera)
        {
            SwitchToCamera(sisterCamera);
        }
    }

    public void ExitToGameCamera()
    {
        if (activeCamera != gameCamera)
        {
            SwitchToCamera(gameCamera);
        }
    }
    public CinemachineVirtualCameraBase GetActiveCamera() => activeCamera;

    // This is what actually triggers the switch when entering a collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchToSisterCamera();
        }
    }
}

