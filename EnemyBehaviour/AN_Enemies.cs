using UnityEngine;

public enum AnimState : int {
	IDLE,
	MOVING_FORWARD,
	STRAFING_LEFT,
	STRAFING_RIGHT,
	FLEEING,
	GROSSED_OUT,
	BACKING_UP,
    CHARGING
}

public class AN_Enemies : MonoBehaviour {

	private AI_Controller 	mEntity;
	private WP_Gun			mGun;

	private Animator		mAnimController;

	private bool		isIdling;
	private bool		isRunningForward;
	private bool		isStrafingLeft;
	private bool		isStrafingRight;
	private bool		isGrossedOut;
	private bool		isFleeing;
	private bool		isBackingUp;

	private bool		isCharging;

	[HideInInspector]
	public AnimState 	mState;

	private void Awake(){
		mAnimController = GetComponent<Animator>();
		mEntity = UT_FindComponent.FindComponent<AI_Controller>(gameObject);
		mGun = UT_FindComponent.FindComponent<WP_Gun>(gameObject);
	}

	public void UpdateAnimations(){

		// first set all states to false, then we set the ones to true that are true.
		SetAllBoolsToFalse();
		SetStateBoolToTrue();
		SetShotTrigger();
		SetAnimationState();
	}

	// so here we have to kind of hack in figuring out when we last shot, and if it's this frame (or very close to)
	private void SetShotTrigger(){
		AI_Conditions cons = mEntity.GetComponent<AI_Conditions>();

		if(Time.time - mGun.mFireTimeStamp <= 0.05f){
			mAnimController.SetTrigger("Shoot");
		}else{
			mAnimController.ResetTrigger("Shoot");
		}
	}

	private void SetAnimationState(){
        // mAnimController.SetBool("isIdling", isIdling);
		// mAnimController.SetBool("isRunningForward", isRunningForward);
        // // mAnimController.SetBool("isStrafing", isStrafing); // implement later.
		// mAnimController.SetBool("isGrossedOut", isGrossedOut);
		// mAnimController.SetBool("isFleeing", isFleeing);
		// mAnimController.SetBool("isCharging", isCharging);

		SetBoolField("isIdling", isIdling);
		SetBoolField("isRunningForward", isRunningForward);
		SetBoolField("isGrossedOut", isGrossedOut);
		SetBoolField("isStrafingLeft", isStrafingLeft);
		SetBoolField("isStrafingRight", isStrafingRight);
		SetBoolField("isFleeing", isFleeing);
		SetBoolField("isCharging", isCharging);
		SetBoolField("isBackingUp", isBackingUp);
	}

	private void SetBoolField(string field, bool val){
		if(HasParameter(field, mAnimController)){
			mAnimController.SetBool(field, val);
		}
	}

	 public static bool HasParameter(string paramName, Animator animator){
		foreach (AnimatorControllerParameter param in animator.parameters){
		if (param.name == paramName)
			return true;
		}
		return false;
	}

	private void SetStateBoolToTrue(){
		switch(mState){
			case AnimState.IDLE: isIdling = true; break;
			case AnimState.MOVING_FORWARD: isRunningForward = true; break;
			case AnimState.STRAFING_LEFT: isStrafingLeft = true; break;
			case AnimState.STRAFING_RIGHT: isStrafingRight = true; break;
			case AnimState.GROSSED_OUT: isGrossedOut = true; break;
			case AnimState.FLEEING: isFleeing = true; break;
			case AnimState.CHARGING: isCharging = true; break;
			case AnimState.BACKING_UP: isBackingUp = true; break;
		}
	}

	private void SetAllBoolsToFalse(){
		isIdling = false;
		isRunningForward = false;
		isStrafingLeft = false;
		isStrafingRight = false;
		isGrossedOut = false;
		isFleeing = false;
		isCharging = false;
		isBackingUp = false;
	}
}
