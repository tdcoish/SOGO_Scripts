using UnityEngine;

public class HoldingObjectAnimationState : StateMachineBehaviour {

	[SerializeField]
	private BooleanVariable isHoldingThrowableObject;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		isHoldingThrowableObject.Value = true;
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		isHoldingThrowableObject.Value = false;
	}

}
