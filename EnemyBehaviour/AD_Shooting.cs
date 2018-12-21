using System.Collections;
using UnityEngine;

public class AD_Shooting : MonoBehaviour {

	[SerializeField]
	private FloatVariable				mDelay;

	public void HandleShooting(){
		SO_AI_Base bs = GetComponent<AI_Controller>().GetBase();
		
		StartCoroutine(PlayDialogueDelayed(bs.mShootLine));
	}

	IEnumerator PlayDialogueDelayed(SO_AD_Dialogue data){

		yield return new WaitForSeconds(mDelay.Value);
		AUD_Manager.DynamicDialogueFromData(data, gameObject);
	}
}
