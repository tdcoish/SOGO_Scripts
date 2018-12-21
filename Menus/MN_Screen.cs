using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*****************************************************************************************
Within the menu, what we do is simply enable and disable the appropriate screen. All screens 
inherit from this class.

No need to explicitly check if a pressed. as long as EventSystem in the scene, that's done for us.
If EventSystem taken out, then explicit check necessary.
*************************************************************************************** */

public abstract class MN_Screen : MonoBehaviour {

	[SerializeField]
	private bool 				mTransitionsNotScroll = false;

	// basically give them their own buttons. So they can scroll through them.
	[SerializeField]
	protected Button[]			mButtons;

	protected int 				mActive;

	// So when we press down on the trigger, we don't scroll through all the buttons. We have a small delay, of 0.25 seconds or so.
	private float				mTimeLastSwitched = 0f;

	private float				mTimeBetweenSwitches = 0.25f;

	[SerializeField]
	protected InputState input;

	[SerializeField]
	protected SO_MN_Events			Events;

	[SerializeField]
	protected bool 				mDisableHorizontalScroll = false;
	[SerializeField]
	protected bool 				mDisableVerticleScroll = false;

	private void OnEnable()
	{
		MNScreenEnabled();	
	}

	private void OnDisable()
	{
		for(int i=0; i<mButtons.Length; i++){
			mButtons[i].transform.localScale = new Vector3(1f, 1f, 1f);
		}
		MakeActiveButtonRightColourOnLeaving();
		MNScreenDisabled();
	}
	
	protected abstract void MNScreenEnabled();
	protected abstract void MNScreenDisabled();

	// Do we have to unselect?
	protected void Update(){
		
		if(mButtons.Length > 0){

			mButtons[mActive].Select();

			// If we can read inputs
			if(Time.time - mTimeLastSwitched > mTimeBetweenSwitches){
				if(!mDisableVerticleScroll){
					if(input.leftStickY < -0.2f){
						ScrollDown();
						return;
					}
					if(input.leftStickY > 0.2f){
						ScrollUp();
						return;
					}
				}

				if(!mDisableHorizontalScroll){
					// testing this semi-hack for now
					if(input.leftStickX > 0.2f){
						ScrollDown();
					}
					if(input.leftStickX < -0.2f){
						ScrollUp();
					}
				}
			}
		}
		
	}

	protected virtual void ScrollDown(){

		mActive++;
		mTimeLastSwitched = Time.time;
		if(mActive == mButtons.Length){
			mActive = 0;
		}

		if(mTransitionsNotScroll){
			Events.OnTransition.Raise(null);
		}else{
			Events.OnScrollDown.Raise(null);
		}
	}

	protected virtual void ScrollUp(){
		
		mActive--;
		mTimeLastSwitched = Time.time;
		if(mActive < 0){
			mActive = mButtons.Length-1;
		}
		
		if(mTransitionsNotScroll){
			Events.OnTransition.Raise(null);
		}else{
			Events.OnScrollUp.Raise(null);
		}
	}

	public string GetActiveButtonText(){
		return mButtons[mActive].GetComponent<Text>().text;
	}

	// public void OnButtonPress(){
	// 	MakeActiveButtonRightColourOnLeaving();
	// }

	protected void MakeActiveButtonRightColourOnLeaving(){
		if(mButtons.Length == 0) return;
		BTN_Select btn = mButtons[mActive].GetComponent<BTN_Select>();
		if(btn != null){
			btn.ResetColourOnLeaving();
		}
	}
}
