using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MN_Options : MN_Screen {

	[SerializeField]
	private GameEvent		OnOptionsActivated;

	private float 			mLastFlip = 0f;
	private float 			mTimeBetweenFlips = 0.25f;
	
	#region Screen Life Cycle

		protected override void MNScreenEnabled(){
			OnOptionsActivated.Raise(null);
		}

		protected override void MNScreenDisabled(){

		}

	#endregion

	protected void Update(){
		base.Update();

		// if we scroll left/right, then do the appropriate thing in the menu
		if(Time.time - mLastFlip > mTimeBetweenFlips){
			if(input.leftStickX > 0.2f){
				FlipRight();
			}
			if(input.leftStickX < -0.2f){
				FlipLeft();
			}
		}
		
		if(input.bButtonPressed){
			Events.OnBackScreen.Raise(null);
			MN_Manager.Instance.ShowScreen<MN_MainMenu>();
		}

		
	}

	private void FlipLeft(){
		mLastFlip = Time.time;
		mButtons[mActive].GetComponent<BTN_Options>().OnPressLeft();
		Events.OnScrollDown.Raise(null);
	}

	private void FlipRight(){
		mLastFlip = Time.time;
		mButtons[mActive].GetComponent<BTN_Options>().OnPressRight();
		Events.OnScrollUp.Raise(null);
	}

}
