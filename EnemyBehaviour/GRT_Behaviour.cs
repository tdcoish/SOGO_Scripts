using UnityEngine;
using System.Collections.Generic;

public enum GRUNT_GOALS : int {

	ATTACK_FROM_RANGE,
    FLEE
};

public class GRT_Behaviour : AI_Controller {

	private GRUNT_GOALS			mCurGoal;

	private GRT_Conditions				mConResults;

	private AI_GrenadedLogic	mGrenadedLogic;

	private AI_AttackFromRange	mAttackRangedLogic;
	private AI_Retreat			mRetreatLogic;
	private AN_Enemies			mAnim;

	private AI_FlyLogic			mFlyLogic;


	private void Start(){
		mGrenadedLogic = GetComponent<AI_GrenadedLogic>();
		mConResults = GetComponent<GRT_Conditions>();
		mAttackRangedLogic = GetComponent<AI_AttackFromRange>();
		mRetreatLogic = GetComponent<AI_Retreat>();
		mAnim = UT_FindComponent.FindComponent<AN_Enemies>(gameObject);
		mFlyLogic = GetComponent<AI_FlyLogic>();
	}

	public override void RunUpdate(bool canGenNodePath){
		mMoveToGoal.mCanGenPathThisFrame = canGenNodePath;

		SmallUpdate();
		
		switch (mState){

			case STATE.FLYING: mFlyLogic.Run(); break;
			case STATE.GRENADED: mGrenadedLogic.Run(); break;
			case STATE.COMBAT: RunCombatState(); break;
			case STATE.IDLE: RunIdleState(); break;
			case STATE.DEAD: RunDeadState(); break;
		}

		mAnim.UpdateAnimations();
    }

	public void RunIdleState(){ }
	public void RunDeadState() {}
	
	public void RunCombatState(){
        rBody.constraints = RigidbodyConstraints.None;

        mConResults.CheckAndStoreConditions();

		// Here we find out what our goal is, to attack from a distance, or to attack with a charge.
        mCurGoal = FindCombatGoal();

		// Here we perform that goal, attacking from a distance or attacking with a charge
		DoCombatGoal(mCurGoal);

		// goal is updated with side effects
		mMoveToGoal.MoveToGoal();
    }

	// Grunts attack from range. They will also flee when the player gets too close.
	private GRUNT_GOALS FindCombatGoal(){

		GRUNT_GOALS gls;

        if(mRetreatLogic.mIsFleeing){
			gls = GRUNT_GOALS.FLEE;
		}else{
			if(mConResults.mDisToPlayer < mBase.mFleeRange.Value && mConResults.mNumAlliesWithinRadius < mBase.mFleeNumAllies && mRetreatLogic.mCanFlee && mConResults.mCanSeePlayer){
				gls = GRUNT_GOALS.FLEE;
			}else{
				gls = GRUNT_GOALS.ATTACK_FROM_RANGE;
			}
		}

        return gls;
	}

	private void DoCombatGoal(GRUNT_GOALS goal){
		switch (goal){
			case GRUNT_GOALS.ATTACK_FROM_RANGE: mAttackRangedLogic.Run(); break;
            case GRUNT_GOALS.FLEE: mRetreatLogic.Run(); break;
			default: break;
		}
	}

}
