using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider))]
public class EventTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (onTriggerEnter != null)
        {
            if(other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
            {
                onTriggerEnter?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (onTriggerExit != null)
        {
            if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
            {
                onTriggerExit?.Invoke();
            }
        }
    }
}
