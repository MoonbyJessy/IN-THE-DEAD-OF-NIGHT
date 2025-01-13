using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;



public class TriggerCamera1 : MonoBehaviour
{
    public GameObject SisterCamera;

    [SerializeField]
    private InputAction action;

    [SerializeField]
    private CinemachineVirtualCamera vcam1; //Game camera

    [SerializeField]
    private CinemachineVirtualCamera vcam2; // Sister camera

    private bool GameCamera = true;

    private void Start()
    {
        action.performed += _ => SwitchPriority();
    }

    private void OnEnable()
    {
        action.Enable();
    }
    private void OnDisable()
    {
        action.Disable();
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
}
