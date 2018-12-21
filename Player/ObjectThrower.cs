using System.Collections;
using UnityEngine;

public class ObjectThrower : MonoBehaviour {

	[SerializeField]
	private PlayerState playerState;
	[SerializeField]
    private BooleanVariable disableInput;
	[SerializeField]
	private InputState input;
	[SerializeField]
	private GameEvent onObjectThrown;
	[SerializeField]
    private GameEvent onPlayerStoppedAiming;
	[SerializeField]
	private BooleanVariable isHoldingThrowableObject; // This is used to show the arch or not
	[SerializeField]
	private FloatVariable throwingForce;
	[SerializeField]
	private LayerMask layerMask;
	[SerializeField]
	private float yAimOffset;
	[SerializeField]
	public float throwingAngle = -10f;

	private ThrowableObject throwableObject = null;
	private Camera mainCam;
	private Vector3 lastAimPoint;
	private PlayerAnimationController animationController;

	private void Awake() {
		animationController = GetComponentInParent<PlayerAnimationController>();
		mainCam = Camera.main;
		playerState.throwingObject = false;
		ResetState();
	}

	private void ResetState() {
		isHoldingThrowableObject.Value = false;
		throwingForce.Value = 0f;
		throwableObject = null;
		transform.rotation = Quaternion.Euler(Vector3.zero);
	}
	
	public void OnObjectPickedUp(object data) {
		throwableObject = playerState.carryingThrowableObject;
		throwableObject.PickUp(transform);
	}

	private void Update() {
		if (disableInput.Value) return;
		
		if (throwableObject != null && isHoldingThrowableObject.Value == true) {	
			throwingForce.Value = throwableObject.properties.throwingForce;
			Aim();
			if (input.yButtonPressed || input.leftTrigger != 0 || input.rightTrigger != 0) {
				throwableObject.Throw(transform.forward);
				onObjectThrown.Raise(null);	
				onPlayerStoppedAiming.Raise(false);
				animationController.PlayThrowObjectAnimation();
				ResetState();
			}
		}
		
	}

	private void Aim() {
		Vector3 camForward = mainCam.transform.forward;
		camForward = Quaternion.AngleAxis(throwingAngle, transform.right) * camForward;
		transform.forward = camForward;
	}

	public void DropObject(object data) {
		if (throwableObject != null) {
			throwableObject.Drop();	
		}
		onObjectThrown.Raise(null);	
		onPlayerStoppedAiming.Raise(false);
		playerState.throwingObject = false;
		ResetState();
	}

	public void FinishThrowingState(object data) {
		playerState.throwingObject = false;
	}
}
