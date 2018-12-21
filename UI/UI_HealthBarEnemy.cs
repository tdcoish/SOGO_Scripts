using UnityEngine;
using UnityEngine.UI;
/********************************************************************************************************
Terribly named. This also affects the weapon charge bar.
******************************************************************************************************* */
public class UI_HealthBarEnemy : MonoBehaviour {

	[SerializeField]
	private Image		mBarImg;

	private AI_Controller		mOwner;
	
	private void Awake(){
		mOwner = UT_FindComponent.FindComponent<AI_Controller>(gameObject);
	}

	public void SetGraphics(float health){
		// first set health bar length.
        mBarImg.fillAmount = health*0.01f;
	}

	private void Update(){
		// first set health bar length.
		float percent = mOwner.GetHealth() / mOwner.GetBase().mMaxHealth;
        mBarImg.fillAmount = percent;
	}
}
