// Kept strictly as an example 

// using System;
// using UnityEngine;


// [CreateAssetMenu(fileName="AI_Move_Goal", menuName="SunsOutGunsOut/AI/TNK_Move_Goal")]
// public class TNK_MOV_GOAL : SO_AI_Move_Goal {

// 	// Why would I need this? I should just need if we're charging or not.
// 	[Header("Bonus for whether we're charging or not.")]
// 	public float		IsChargePlus;
// 	public float		NotChargingPlus;

// 	public override float ScoreConditions(AI_Conditions cons){

// 		float score = 0f;
// 		base.ScoreConditions(cons);

// 		// gotta cast the cons as TNK_COnditions 
// 		TNK_Conditions tnkCons = cons.GetComponent<TNK_Conditions>();

// 		// now we get whether or not we're currently charging. If so, add IsChargePlus, if not, add NotChargingPlus
// 		bool isCharging = tnkCons.GetComponent<TNK_Behaviour>().mIsCharging;

// 		if(isCharging){
// 			score += IsChargePlus;
// 		}else{
// 			score += NotChargingPlus;
// 		}

// 		return score;
// 	}

// }
