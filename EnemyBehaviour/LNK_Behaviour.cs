using UnityEngine;
using System.Collections.Generic;

public enum LANKY_GOALS : int {

	ATTACK_FROM_RANGE
};

public class LNK_Behaviour : AI_Controller {

	private LNK_Conditions				mConResults;

	private LANKY_GOALS			mCurGoal;

	private AI_GrenadedLogic	mGrenadedLogic;

	private AI_Strafer			mStrafer;
	private AI_AttackFromRange	mAttackFromRangeLogic;
	private AN_Enemies			mAnim;

	private EnemyForceField		mShield;

	private void Start(){
		mGrenadedLogic = GetComponent<AI_GrenadedLogic>();
		mConResults = GetComponent<LNK_Conditions>();
		mStrafer = GetComponent<AI_Strafer>();
		mAttackFromRangeLogic = GetComponent<AI_AttackFromRange>();
		mAnim = UT_FindComponent.FindComponent<AN_Enemies>(gameObject);
		mShield = GetComponentInChildren<EnemyForceField>();
	}

	public override void RunUpdate(bool canGenNodePath){
		mMoveToGoal.mCanGenPathThisFrame = canGenNodePath;		

		SmallUpdate();
		
		switch (mState){

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

        CheckAndHandleDeath();
    }

	// For now all these idiots do is follow the player and attack him.
	private LANKY_GOALS FindCombatGoal(){

		LANKY_GOALS gls;

        gls = LANKY_GOALS.ATTACK_FROM_RANGE;
        return gls;
	}

	private void DoCombatGoal(LANKY_GOALS goal){
		switch (goal){
			case LANKY_GOALS.ATTACK_FROM_RANGE: mAttackFromRangeLogic.Run(); break;
			default: break;
		}
	}

}
