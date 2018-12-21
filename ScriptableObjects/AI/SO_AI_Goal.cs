using UnityEngine;

/*********************************************************************************************
This is a template for a goal. A goal entity uses this when comparing the results of conditions
to this.
******************************************************************************************* */
public class SO_AI_Goal : ScriptableObject {

	[Header("Function to call")]
	public string FunctionName;

	[Header("Default Bonus")]
	public float 		DefaultPlus;

	[Header("Bonus to goal for seeing player")]
	public float 		CanSeePlayerPlus;
	public float 		CantSeePlayerPlus;

	[Header("Bonus for distance from player")]
	public float 		DistanceToCare;
	public float 		WithinDistancePlus;
	public float 		OutsideDistancePlus;

	[Header("Bonus for distance in relation to gun range")]
	public float 		RangeMultiplier;
	public float		WithinRangeMultiplierPlus;
	public float		OutsideRangeMultiplerPlus;

	[Header("Bonus for being in range to fire gun")]
	public float		InGunRangePlus;
	public float		OutOfGunRangePlus;

	[Header("Bonus for just firing gun")]
	public float 		TimeToCareAbout;
	public float		WithinTimePlus;
	public float		NotWithinTimePlus;


	// protected float ScoreConditions(AI_Conditions cons) { 
		
	// }

	public string GetGoalCommandName(){
		return FunctionName;
	}

}
