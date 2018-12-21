/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private PlayerState playerState;
    [SerializeField]
    private FloatVariable freeMovementSpeed;
    [SerializeField]
    private FloatVariable strafeMovementSpeed;
    [SerializeField]
    private float rotationSpeed = 20f;
    [SerializeField]
    private float rotationSpeedWhenSliding = 5f;
    [SerializeField]
    private InputState input;
    [SerializeField]
    private BooleanVariable disableInput;
    [SerializeField]
    private BooleanVariable isPlayerSliding;


    private bool isAiming = false;
    private Vector3 moveInput;
    internal Vector3 moveVelocity { get; private set; }
    private Rigidbody rb;

    private Camera mainCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (disableInput.Value || playerState.pickingUpObject) {
            moveVelocity = Vector3.zero;
            return;
        }
        Move();
    }

    public void OnPlayerAiming (object data) {
		isAiming = (bool) data;
	}
    
    private void Move()
    {
        moveInput = new Vector3(input.leftStickX, 0, input.leftStickY);
        if (isAiming == false) {
            FreeMovement();
        } else {
            StrafeMovement();
        }
    }

    private void StrafeMovement()
    {
        transform.forward = mainCamera.transform.forward;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        Vector3 newVelocity = transform.right * input.leftStickX * strafeMovementSpeed.Value;
        newVelocity += transform.forward * input.leftStickY * strafeMovementSpeed.Value;
        moveVelocity = newVelocity;
    }

    private void FreeMovement() {
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;        
        Quaternion cameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, cameraForward);
        Vector3 lookToward = cameraRelativeRotation * moveInput.normalized;

        if (moveInput.sqrMagnitude > 0)
        {
            Quaternion destRotation = Quaternion.LookRotation(lookToward);
            float rotSpeed = isPlayerSliding.Value ? rotationSpeedWhenSliding : rotationSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, destRotation, rotSpeed * Time.fixedDeltaTime);
        }

        moveVelocity = transform.forward * freeMovementSpeed.Value * Mathf.Clamp(moveInput.sqrMagnitude, 0f, 1f);
    }
}