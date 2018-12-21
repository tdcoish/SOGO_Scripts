/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;
using Cinemachine;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour {
	
	// Components
	[SerializeField]
	private PlayerState playerState;
	[SerializeField]
	private VectorVariable playerVelocity;
	private Rigidbody rb;
	private Movement movement;
	private Jump jump;
	private Slide slide;

    private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		movement = GetComponent<Movement>();
		jump = GetComponent<Jump>();
		slide = GetComponent<Slide>();
	}

	private void FixedUpdate()
	{
		HandlePhysicsMovement();
	}

	private void HandlePhysicsMovement()
	{
		Vector3 newVelocity = movement.moveVelocity;
		if (playerState.sliding) {
			newVelocity += rb.transform.forward * slide.slideSpeed;
		}
        newVelocity.y = jump.SetJumpForce(rb.velocity.y);
		playerVelocity.Value = newVelocity;
        rb.velocity = playerVelocity.Value;
        jump.ResetJump();
    }

	// Called by listening to the OnCheckpointRestarted event
	public void MovePlayerTo(Vector3 pos, Quaternion rot){
		transform.position = pos;
		transform.rotation = rot;
	}
}
