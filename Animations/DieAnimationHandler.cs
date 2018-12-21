using UnityEngine;

public class DieAnimationHandler : StateMachineBehaviour {

	[SerializeField]
	private GameEvent onShowDeathScreen;
	[SerializeField]
	private BooleanVariable disableInput;

	private bool hasTriggeredTheEvent = false;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		disableInput.Value = true;
		hasTriggeredTheEvent = false;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (stateInfo.normalizedTime >= 1.2f && hasTriggeredTheEvent == false) {
			hasTriggeredTheEvent = true;
			onShowDeathScreen.Raise(null);
		}
	}

}
