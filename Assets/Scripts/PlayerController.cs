using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public VariableJoystick joystick;
    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public float minVerticalAngle = -80f; 
    public float maxVerticalAngle = 80f;  

    private Vector2 touchDelta; 
    private float verticalRotation = 0f; 

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        LookAround();
    }

    private void Move()
    {
        Vector3 moveInput = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        Vector3 moveDir = cameraTransform.TransformDirection(moveInput).normalized;
        
        float currentVerticalVelocity = rb.velocity.y;
        
        rb.velocity = new Vector3(moveDir.x * moveSpeed, currentVerticalVelocity, moveDir.z * moveSpeed);
    }

    private void LookAround()
    {
        if (Input.touchCount > 0  && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            Touch touch = Input.GetTouch(0);
            touchDelta = touch.deltaPosition;
            
            float horizontalRotation = touchDelta.x * lookSpeed;
            transform.Rotate(0, horizontalRotation, 0);

            verticalRotation -= touchDelta.y * lookSpeed; 
            verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle); 

            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }
}
