/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    private PlayerState playerState;
    [SerializeField]
    private BooleanVariable disableInput;
    [SerializeField]
	private InputState input;
    [SerializeField]
    private float jumpForce = 12f;
    [HideInInspector]
    public bool jumpButtonPressed = false;

    private AD_Jump audJump;

    private Grounded grounded;
    private bool groundedCachedValue = true;

    private float lastJumpStartTimestamp = 0f;
    private float jumpCheckTimeWindow = 0.5f;

    private void Awake()
    {
        grounded = GetComponentInChildren<Grounded>();
        audJump = GetComponent<AD_Jump>();
        playerState.jumping = false;
    }

    private void Update()
    {
        playerState.grounded = grounded.IsGrounded();
        HandleLanding();
        if (disableInput.Value || playerState.pickingUpObject) return;
        jumpButtonPressed = input.aButtonPressed;
    }

    private void HandleLanding()
    {
        if (playerState.grounded && groundedCachedValue != playerState.grounded) {
            playerState.airDeflect = false;
        }
        
        if (playerState.jumping && playerState.grounded && (groundedCachedValue != playerState.grounded || (Time.time >= lastJumpStartTimestamp + jumpCheckTimeWindow && groundedCachedValue == playerState.grounded)))
        {
            audJump.HandleJump();
            playerState.jumping = false;
            playerState.airDeflect = false;
        }
        
        groundedCachedValue = playerState.grounded;
    }

    public void ResetJump()
    {
        jumpButtonPressed = false;
    }

    public float SetJumpForce(float currentYVelocity)
	{
		if (jumpButtonPressed && playerState.grounded)
		{
            playerState.jumping = true;
            audJump.HandleLand();
            lastJumpStartTimestamp = Time.time;
			return jumpForce;
		}
		else 
		{
			return currentYVelocity;
		}
	}
}