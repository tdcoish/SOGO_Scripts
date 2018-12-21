using UnityEngine;
using UnityEngine.UI;

/***************************************************************************************************
This class controls the UI canvas that is related to the player. It will be pinned to the screen.
*************************************************************************************************** */
public class UI_Player : MonoBehaviour {

	[SerializeField]
	private PlayerState					mPlayerState;
	
	[SerializeField]
	private FloatVariable				mHealth;
	[SerializeField]
	private FloatVariable				mStamina;

	[SerializeField]
	private BooleanVariable				mCanSlide;
	[SerializeField]
	private FloatVariable				mSlideAmount;

	[SerializeField]
	private Image						mHStaminaFill;
	[SerializeField]
	private Image						mHBarFill;

	[SerializeField]
	private Image 						mSlideEnabled;
	[SerializeField]
	private Image						mSlideActive;
	[SerializeField]
	private Image						mSlideBarFill;

	private void Update(){
		// Set health bar fill amount.
		mHBarFill.fillAmount = mHealth.Value*0.01f;
		mHStaminaFill.fillAmount = mStamina.Value;
		SetSlideImages();
	}

	private void SetVisibilityOfImage(Image img, bool makeActive){

		Color tempColor = img.color;
		if(makeActive){
			tempColor.a = 1f;
		}else{
			tempColor.a = 0f;
		}

		img.color = tempColor;
	}

	private void SetSlideImages(){
		SetVisibilityOfImage(mSlideEnabled, mCanSlide.Value);
		SetVisibilityOfImage(mSlideActive, mPlayerState.sliding);

		mSlideBarFill.fillAmount = mSlideAmount.Value;
	}
}
