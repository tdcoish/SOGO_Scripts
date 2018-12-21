using System.Collections;
using UnityEngine;

public class ObjectPicker : MonoBehaviour {

	[SerializeField]
	private PlayerState playerState;
	[SerializeField]
    private BooleanVariable disableInput;
	[SerializeField]
	private InputState input;
	[SerializeField]
	private InGameButtonIndicator buttonIndicator;

	[SerializeField]
	private GameEvent	OnObjectPickedUp;

	private CapsuleCollider coll;

	private void Awake() {
		coll = GetComponent<CapsuleCollider>();
		playerState.pickingUpObject = false;
		playerState.throwingObject = false;
		coll.isTrigger = true;
		coll.enabled = true;
		buttonIndicator = transform.parent.GetComponentInChildren<InGameButtonIndicator>();
	}

	private void PickUpObject(ThrowableObject objectTryingToPickup) {
		coll.enabled = false;
		buttonIndicator.indicator.enabled = false;
		playerState.throwingObject = true;
		playerState.pickingUpObject = true;
		playerState.carryingThrowableObject = objectTryingToPickup;
		OnObjectPickedUp.Raise(objectTryingToPickup);
	}

	public void OnObjectThrown(object data) {
		StartCoroutine(EnablePicker()); // We need to wait for one frame so the player can't pickup another object immediately
	}
	
	private void OnTriggerStay (Collider other) {
		ThrowableObject objectTryingToPickup = other.GetComponentInParent<ThrowableObject>();
		if (objectTryingToPickup != null && objectTryingToPickup.hasBeenThrown == false && disableInput.Value == false && playerState.deflecting == false && playerState.sliding == false && playerState.throwingGrenade == false && playerState.meleeing == false) {
			buttonIndicator.indicator.enabled = true;
			if (input.yButtonPressed && playerState.grounded && playerState.jumping == false) {
				PickUpObject(objectTryingToPickup);	
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		ThrowableObject objectTryingToPickup = other.GetComponentInParent<ThrowableObject>();
		if (objectTryingToPickup != null) {
			buttonIndicator.indicator.enabled = false;
		}
	}

	IEnumerator EnablePicker() {
		yield return null;
		coll.enabled = true;
		yield break;
	}

	public void DropObject(object data) {
		if (playerState.pickingUpObject) {
			playerState.pickingUpObject = false;
			playerState.throwingObject = false;
			buttonIndicator.indicator.enabled = false;
			coll.enabled = true;
			if (playerState.carryingThrowableObject != null) {
				playerState.carryingThrowableObject.Drop();
			}
		}
	}
}
