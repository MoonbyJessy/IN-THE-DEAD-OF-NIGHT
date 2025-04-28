using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EventTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    [SerializeField] private CameraSwitcher cameraSwitcher;
    private PlayerMovement playerMovement;
    private Collider triggerCollider;
    private bool shouldDisableAfterReturn = false;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();

        if (cameraSwitcher == null)
            cameraSwitcher = FindObjectOfType<CameraSwitcher>();
    }

    private void OnEnable()
    {
        if (cameraSwitcher != null)
            cameraSwitcher.onSwitchCamera += HandleCameraSwitch;
    }

    private void OnDisable()
    {
        if (cameraSwitcher != null)
            cameraSwitcher.onSwitchCamera -= HandleCameraSwitch;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out playerMovement))
        {
            shouldDisableAfterReturn = true;
            onTriggerEnter?.Invoke();
            playerMovement.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement pm))
        {
            onTriggerExit?.Invoke();
        }
    }

    private void HandleCameraSwitch()
    {
        if (shouldDisableAfterReturn &&
            cameraSwitcher.GetActiveCamera() == cameraSwitcher.gameCamera)
        {
            if (playerMovement != null)
                playerMovement.enabled = true;

            triggerCollider.enabled = false;
            this.enabled = false;

        }
    }
}
