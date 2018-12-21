using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimationState : StateMachineBehaviour {

	[SerializeField]
	private PlayerState playerState;
	[SerializeField]
	private GameEvent onStartLiftingObject;
	[SerializeField]
    private GameEvent onPlayerStartedAiming;

	private bool hasStartedLiftingObject = false; 


	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if ((stateInfo.normalizedTime >= 0.5f && hasStartedLiftingObject == false && playerState.carryingThrowableObject != null) || playerState.grounded == false) {
			hasStartedLiftingObject = true;
			onStartLiftingObject.Raise(null);
		}

		if (stateInfo.normalizedTime >= 1f || playerState.grounded == false) {
			playerState.pickingUpObject = false;
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (playerState.throwingObject) {
			animator.SetTrigger("PickupObject");
			onPlayerStartedAiming.Raise(true);
		}
		hasStartedLiftingObject = false;
	}

}
