using UnityEngine;
using UnityEngine.UI;

/**************************************************************************************************
Credits screen now has a bunch of features, such as gamma control, control inversion, Volume, Music ON/OFF.
************************************************************************************************ */
public class MN_Credits : MN_Screen {

	[SerializeField]
	private GameEvent		OnPageActivated;

	// [SerializeField]
	// private MN_Screen		mPageUp;
	// [SerializeField]
	// private MN_Screen 		mPageDown;

	protected bool 			mCanFlip;

	#region Screen Life Cycle

		protected override void MNScreenEnabled(){
			OnPageActivated.Raise(null);
			mCanFlip = false;
			Invoke("AllowFlipPage", 0.2f);

			for(int i=0; i<mButtons.Length; i++){
				if(i != mActive){
					mButtons[i].GetComponent<BTN_Select>().ResetColourOnLeaving();
				}
			}
		}

		protected override void MNScreenDisabled(){

		}

	#endregion

	protected void Update(){
		base.Update();

		// if we scroll left/right, then do the appropriate thing in the menu
		if(mCanFlip){
			if(input.leftStickY > 0.2f){
				PageUp();
			}
			if(input.leftStickY < -0.2f){
				PageDown();
			}
		}

		if(input.bButtonPressed){
			Events.OnBackScreen.Raise(null);
			MN_Manager.Instance.ShowScreen<MN_MainMenu>();
		}
	}

	protected virtual void PageDown(){}
	protected virtual void PageUp() {}

	protected void AllowFlipPage(){
		mCanFlip = true;
	}

}
