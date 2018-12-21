using UnityEngine;

[CreateAssetMenu(fileName="NewAIProfile", menuName="SunsOutGunsOut/AI/NewAIProfile")]
public class SO_AI_Profile : ScriptableObject {

	[Header("Bias towards moving the entity towards it's target")]
	public float				mTargetForceWeight;

	[Header("Bias towards moving entity away from other very close entities")]
	public float				mSepDis;
	public float				mSepForceWeight;

	[Header("Bias towards keeping the heading of the group, and towards center of mass")]
	public float				mGrpDis;
	public float				mHeadForceWeight;
	public float				mGroupForceWeight;

	[Header("Bias away from objects to avoid")]
	public float				mLvlForceWeight;

	[Header("Distance from target to start slowing down by")]
	public float				mSlowDis;
}
