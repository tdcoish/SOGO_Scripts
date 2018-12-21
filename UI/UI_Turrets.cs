using UnityEngine;
using UnityEngine.UI;
/********************************************************************************************************
Terribly named. This also affects the weapon charge bar.
******************************************************************************************************* */
public class UI_Turrets : MonoBehaviour {

	[SerializeField]
	private Image		mChargeImg;
	[SerializeField]
    private Image       mHealthImg;
    
	private AI_Turret		mTurretComp;
	
	private void Awake(){
		mTurretComp = UT_FindComponent.FindComponent<AI_Turret>(gameObject);
	}

	private void Update(){
		// first set health bar length.
        mHealthImg.fillAmount = mTurretComp.mHealth*0.01f;

		// Now set charge bar length
        mChargeImg.fillAmount = mTurretComp.mCannon.mCurChargeAmt / mTurretComp.mCannon.mTurretGunProperties.mMaxChargeUpTime;
	}

}
