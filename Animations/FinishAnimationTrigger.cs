using UnityEngine;

public class FinishAnimationTrigger : StateMachineBehaviour {

	[SerializeField]
	private GameEvent onAnimationEndEvent;

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		onAnimationEndEvent.Raise(null);
	}
}
