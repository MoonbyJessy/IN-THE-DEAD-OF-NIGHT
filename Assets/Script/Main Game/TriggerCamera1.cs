using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;



public class TriggerCamera1 : MonoBehaviour
{
    [SerializeField]
    private InputAction switchAction;
    private InputAction exitAction;

    [SerializeField]
    private CinemachineVirtualCameraBase vcam1; //Game camera

    [SerializeField]
    private CinemachineVirtualCameraBase vcam2; // Sister camera

    private bool GameCamera = true;
    private void OnEnable()
    {
        switchAction.Enable();
        exitAction.Enable();
    }

    private void OnDisable()
    {
        switchAction.Disable();
        exitAction.Disable();
    }
    private void Start()
    {
        if (switchAction == null)
        { 
            switchAction = new InputAction(binding: "<Keyboard>/e");
        }
        if (exitAction == null)
        { 
             exitAction = new InputAction(binding: "<Mouse>/leftButton");      
        }
    }
  
    private void SwitchPriority()
    { 
        if (GameCamera)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
        else
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }
        GameCamera = !GameCamera;

    }
    private void SwitchToSisterCamera()
    {
        if (GameCamera)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
            GameCamera = false;
            Debug.Log("Switched to Sister Camera");
        }
    }

    private void TryExitCamera()
    {
        if (!GameCamera)
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
            GameCamera = true;
            Debug.Log("Returned to Game Camera");
        }
    }
    public void TriggerSwitch()
    {
        if (GameCamera)
            SwitchToSisterCamera();
        else
            TryExitCamera();
    }
}
