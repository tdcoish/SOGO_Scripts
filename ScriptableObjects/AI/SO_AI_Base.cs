using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyTypes : int {
    GRUNT,
    LANKY,
    TANK
}

[CreateAssetMenu(fileName="NewAIBase", menuName="SunsOutGunsOut/AI/NewAIBase")]
public class SO_AI_Base : ScriptableObject {

    // Spawned when the player dies.
    public GameObject           mRedDeathParticles;
    public GameObject           mBlueDeathParticles;

    public float                mHeightToGround;

    public GameObject           mRedDeathMark;
    public GameObject           mBlueDeathMark;

    public float               mMaxSpd = 4.5f;

    public float                mMaxHealth = 100f;

    public FloatVariable        mChargeRange;
    public FloatVariable        mChargeTime;

    public FloatVariable        mFleeRange;
    public FloatVariable        mFleeTime;
    public float                mFleeAlliesCalmingRange;
    public int                  mFleeNumAllies;

    public bool                 mFireInaccurately = false;
    public float                mInacRange = 5f;

    public float                mTimeInGrenadeStun = 2f;
    public float                mTimeFly = 4f;

    public float                mStopToFireTime = 0.5f;

    public AI_ScreamFX          mDeathScream;
    public SO_AD_Dialogue       mDeathDialogue;
    public SO_AD_Dialogue       mGeneralDamage;

    public SO_AD_Dialogue       mShootLine;

    public float                mRushTime = 0f;

    public EnemyTypes           mType;

    public GameEvent            mEnemyFired;

	[Header("The range within which we fire independently of the AI_Master")]
    public float                mAlwaysFireRange;

    public bool                 mCanFireGun = true;
    
}
