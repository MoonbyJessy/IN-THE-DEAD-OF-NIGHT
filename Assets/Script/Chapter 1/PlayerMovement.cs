using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float gravityForce = -1.5f;

    public bool isSprinting;
    public bool isWalking;

    private Vector3 velocity;


    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    public KeyCode sprintKey = KeyCode.LeftShift;

    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveDirection = Vector3.zero;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            isWalking = true;

            if (Input.GetKey(sprintKey))
            {
                isSprinting = true;
                moveSpeed = sprintSpeed;
            }
            else
            {
                isSprinting = false;
                moveSpeed = walkSpeed;
            }
        }
        else
        {
            isWalking = false;
            isSprinting = false;
            moveSpeed = 0f;
        }
        velocity.y = gravityForce;
        controller.Move((moveDirection.normalized * moveSpeed + velocity) * Time.deltaTime);

        animator.SetBool("isSprinting", isSprinting);
        animator.SetBool("isWalking", isWalking);
    }
}
