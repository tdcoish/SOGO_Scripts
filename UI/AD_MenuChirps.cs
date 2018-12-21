using UnityEngine;

public class AD_MenuChirps : MonoBehaviour {


	public void OnScreenTransitioned(){
		AUD_Manager.PostEvent("FE_Transitions_ST", gameObject);
	}

	public void OnBackScreen(){
		AUD_Manager.PostEvent("FE_UI_Back_ST", gameObject);
	}

	public void OnButtonPress(){
		AUD_Manager.PostEvent("FE_UI_Enter_ST", gameObject);
	}

	public void OnScrollUp(){
		AUD_Manager.PostEvent("FE_UI_Down_ST", gameObject);
	}

	public void OnScrollDown(){
		AUD_Manager.PostEvent("FE_UI_Up_ST", gameObject);			
	}
}
