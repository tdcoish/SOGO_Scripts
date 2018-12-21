using Photon.Pun;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviourPun {
	
	[SerializeField]
	private InputState input;
	[SerializeField]
    private BooleanVariable disableInput;
	[SerializeField]
	internal PlayerState playerState;
	[SerializeField]
	private VectorVariable playerVelocity;
	[SerializeField]
	private BooleanVariable blueDeflectorActive;
	[SerializeField]
	private BooleanVariable redDeflectorActive;
	[SerializeField]
	private BooleanVariable isHoldingThrowableObject;
	
	private Animator animator;

	private void Awake() {
		animator = GetComponent<Animator>();
	}
	
	private void Update() {
		animator.SetBool("IsSliding", playerState.sliding);
		Vector3 speed = playerVelocity.Value;
		speed.y = 0f;
		animator.SetFloat("Speed", speed.magnitude.Normalize(0, 8f));
		animator.SetBool("Flexing", redDeflectorActive.Value);
		animator.SetBool("Posing", blueDeflectorActive.Value);
		animator.SetBool("AirDeflect", playerState.airDeflect);
		animator.SetBool("PickingUpObject", playerState.pickingUpObject);
		animator.SetBool("IsHoldingThrowableObject", isHoldingThrowableObject.Value);
		animator.SetBool("IsJumping", playerState.jumping);
		animator.SetBool("IsGrounded", playerState.grounded);
		animator.SetBool("IsMeleeing", playerState.meleeing);
		animator.SetFloat("VerticalVelocity", playerVelocity.Value.y.Normalize(-1f, 12f));
		animator.SetBool("IsChargingGrenade", playerState.throwingGrenade);
		animator.SetBool("IsDead", playerState.isPlayerDead);
		
		// Strafe movement control
		if (disableInput.Value == false) {
			animator.SetFloat("ForwardVelocity", input.leftStickY);
			animator.SetFloat("SideVelocity", input.leftStickX);
		} else {
			animator.SetFloat("ForwardVelocity", 0f);
			animator.SetFloat("SideVelocity", 0f);	
		}
	}

	public void PlayMeleeAnimation() {
		animator.SetTrigger("Melee");
	}

	public void PlayThrowObjectAnimation() {
		animator.SetTrigger("ThrowObject");
	}

	public void PlayChargeGrenadeAnimation() {
		animator.SetTrigger("ChargeGrenadeThrow");
	}

	public void PlayDamageAnimation(object data) {
		animator.SetTrigger("TakeDamage");
	}
}