using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript2 : MonoBehaviour
{
    public CharacterController controller;



    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;


    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    public KeyCode sprintKey = KeyCode.LeftShift;






    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKey(sprintKey))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        //Rotation Code From Link //https://www.youtube.com/watch?v=4HpC--2iowE 

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
        }
        controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime + Physics.gravity * Time.deltaTime);
    }
}
