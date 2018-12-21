using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponCharge : MonoBehaviour {

	[SerializeField]
	private Image		mChargeImg;

	private WP_Gun			mGun;

	private void Awake(){
		mGun = UT_FindComponent.FindComponent<WP_Gun>(gameObject);
	}

	// expect a value between 0-1.
	public void SetGraphics(float charge){
		mChargeImg.fillAmount = charge;
		mChargeImg.fillAmount = 0f;

	}

	private void Update(){
		mChargeImg.fillAmount = mGun.GetNormalizedCharge();
		mChargeImg.fillAmount = 0f;
	}
}
