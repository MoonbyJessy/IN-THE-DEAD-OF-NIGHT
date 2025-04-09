using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCameraBase gameCamera;
    public CinemachineVirtualCameraBase activeCamera;
    public Action onSwitchCamera;

    public void SwitchToCamera(CinemachineVirtualCameraBase virtualCamera)
    {
        activeCamera.Priority = 0;
        virtualCamera.Priority = 1;
        activeCamera = virtualCamera;
        onSwitchCamera?.Invoke();
    }
    public void SwitchToGameCamera()
    {
        SwitchToCamera(gameCamera);
    }

    public CinemachineVirtualCameraBase GetActiveCamera() { return activeCamera; }

}
