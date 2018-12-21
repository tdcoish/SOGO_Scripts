using UnityEngine;

public class RandomAnimationStateMachine : StateMachineBehaviour {

	[SerializeField]
	private int availableAnimations = 2;
	[SerializeField]
	private string animationID = null;

	private int lastPlayedAnimation = -1;
	private int nextAnimation = -1;

	// OnStateMachineEnter is called when entering a statemachine via its Entry Node
	override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
		if (animationID == null) {
			Debug.LogWarning("No random animation ID string defined!!");
			return;
		}

		do {
			nextAnimation = Random.Range(0, availableAnimations);
		} while (lastPlayedAnimation == nextAnimation);
		lastPlayedAnimation = nextAnimation;
		animator.SetInteger(animationID, nextAnimation);
	}
}
