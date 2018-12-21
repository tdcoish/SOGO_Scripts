// using System;
// using UnityEngine;


// public class SO_AI_Move_Goal : SO_AI_Goal {

// 	[Header("Bonus to goal for seeing player")]
// 	public float 		CanSeePlayerPlus;
// 	public float 		CantSeePlayerPlus;

// 	[Header("Bonus for distance from player")]
// 	public float 		DistanceToCare;
// 	public float 		WithinDistancePlus;
// 	public float 		OutsideDistancePlus;

// 	[Header("Bonus for distance in relation to gun range")]
// 	public float 		RangeMultiplier;
// 	public float		WithinRangeMultiplierPlus;
// 	public float		OutsideRangeMultiplerPlus;


// 	[Header("Bonus for being in range to fire gun")]
// 	public float		InGunRangePlus;
// 	public float		OutOfGunRangePlus;

// 	[Header("Bonus for just firing gun")]
// 	public float 		TimeToCareAbout;
// 	public float		WithinTimePlus;
// 	public float		NotWithinTimePlus;

// 	public override float ScoreConditions(AI_Conditions cons){
// 		float score = 0f;
		
// 		score += DefaultPlus;
		 
// 		// Have to add second field for negative if condition not met.
// 		if(cons.mCanSeePlayer){
// 			score += CanSeePlayerPlus;
// 		}else{
// 			score += CantSeePlayerPlus;
// 		}
		
// 		if(cons.mDisToPlayer < DistanceToCare){
// 			score += WithinDistancePlus;
// 		}else{
// 			score += OutsideDistancePlus;
// 		}

// 		if(cons.mDisToPlayer < cons.mGunRange*RangeMultiplier){
// 			score += WithinRangeMultiplierPlus;
// 		}else{
// 			score += OutsideRangeMultiplerPlus;
// 		}

// 		if(cons.mWithinGunRange){
// 			score += InGunRangePlus;
// 		}else{
// 			score += OutOfGunRangePlus;
// 		}

// 		if(Time.time - cons.mGunFireTimeStamp < TimeToCareAbout){
// 			score += WithinTimePlus;
// 		}else{
// 			score += NotWithinTimePlus;
// 		}

// 		return score;
// 	}

// }
