using UnityEngine;

public class RandomClipBlendTree : StateMachineBehaviour {
	
	[SerializeField]
	private int animations;
	[SerializeField]
	private string blendTreeParameter = null;
	[Space]
	[SerializeField]
	private bool debugMode = false;
	[SerializeField]
	private int debugClip = 0;

	private int lastPlayedAnimation = -1;
	private int nextAnimation = -1;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		float animationValue = 0f;

		if (debugMode) {
			animationValue = debugClip * (1f / (animations - 1));
		} else {
			do {
				nextAnimation = Random.Range(0, animations);
			} while (lastPlayedAnimation == nextAnimation);
			lastPlayedAnimation = nextAnimation;
			animationValue = nextAnimation * (1f / (animations - 1));
		}

		animator.SetFloat(blendTreeParameter, animationValue);
	}
}
