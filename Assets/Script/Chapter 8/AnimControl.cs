using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E)) // Check if 'E' key is being held
        {
            animator.SetTrigger("ChestOpen"); // Trigger the animation
        }
    }
}
