using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PickupScript : MonoBehaviour
{
    [SerializeField] private InputAction press;
    [SerializeField] private InputAction screenPos;

    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform inspectPoint;

    [SerializeField] private float dragStrength = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float transitionThreshold = 0.05f;
    [SerializeField] private float inspectLerpSpeed = 2f;

    private Rigidbody rb;
    private Camera cam;
    private bool isDragging;
    private bool isAtInspectPoint = false;
    private Vector2 curScreenPos;

    private static PickupScript currentDraggedObject;
    private static bool isPickupLocked = false;

    private float currentXRotation = 0f;
    private float currentYRotation = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        screenPos.Enable();
        press.Enable();

        screenPos.performed += ctx => curScreenPos = ctx.ReadValue<Vector2>();
        press.performed += _ => HandlePickUp();
        press.canceled += _ => ReleaseItem();
    }

    private void OnEnable()
    {
        press.Enable();
        screenPos.Enable();
    }

    private void OnDisable()
    {
        press.Disable();
        screenPos.Disable();

        if (currentDraggedObject == this)
            currentDraggedObject = null;

        isPickupLocked = false;
    }


    private void HandlePickUp()
    {
        if (currentDraggedObject != null || isPickupLocked) return;

        if (IsClickedOn())
        {
            StartCoroutine(Drag());
        }
    }

    private bool IsClickedOn()
    {
        Ray ray = cam.ScreenPointToRay(curScreenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == transform;
        }
        return false;
    }

    private IEnumerator Drag()
    {
        isDragging = true;
        isPickupLocked = true;
        currentDraggedObject = this;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.freezeRotation = true;

        while (Vector3.Distance(rb.position, holdPoint.position) > transitionThreshold)
        {
            rb.velocity = (holdPoint.position - rb.position) * dragStrength;
            ApplyRotation();
            HandleRotationInput();
            yield return null;
        }

        rb.position = holdPoint.position;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.1f);

        float time = 0f;
        Vector3 initialPosition = rb.position;
        while (Vector3.Distance(rb.position, inspectPoint.position) > transitionThreshold)
        {
            time += Time.deltaTime * inspectLerpSpeed;
            rb.position = Vector3.Lerp(initialPosition, inspectPoint.position, time);
            ApplyRotation();
            HandleRotationInput();
            yield return null;
        }

        rb.position = inspectPoint.position;
        rb.velocity = Vector3.zero;
        isAtInspectPoint = true;

        while (isDragging)
        {
            ApplyRotation();
            HandleRotationInput();
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        isPickupLocked = false;
    }

    private void ReleaseItem()
    {
        isDragging = false;
        currentDraggedObject = null;

        rb.useGravity = true;
        rb.freezeRotation = false;
        rb.velocity = Vector3.zero;
    }

    private void HandleRotationInput()
    {
        float yDelta = 0f;
        float xDelta = 0f;

        if (Keyboard.current.qKey.isPressed)
            yDelta -= rotationSpeed * Time.deltaTime;
        if (Keyboard.current.eKey.isPressed)
            yDelta += rotationSpeed * Time.deltaTime;
        if (Keyboard.current.wKey.isPressed)
            xDelta -= rotationSpeed * Time.deltaTime;
        if (Keyboard.current.sKey.isPressed)
            xDelta += rotationSpeed * Time.deltaTime;

        currentYRotation += yDelta;
        currentXRotation += xDelta;
    }

    private void ApplyRotation()
    {
        Quaternion desiredRotation = Quaternion.Euler(currentXRotation, currentYRotation, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 5f);
    }
}

