using UnityEngine;

/********************************************************************
Handles audio for the slide.
******************************************************************** */
public class AD_Slide : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue		mSlideStart;

	[SerializeField]
	private SO_AD_Dialogue		mSlideStop;

	[SerializeField]
	private StringVariable		Player;

	public void HandleStartSliding(){
		AUD_Manager.PostEvent("PC_SlideStart_ST", gameObject);

		// Play some dialogue line when we start.
		AUD_Manager.PlayerDialogue(mSlideStart, gameObject, Player.Value);
	}

	public void HandleStopSliding(){
		AUD_Manager.PostEvent("PC_SlideStop_ST", gameObject);

		// Play dialogue for when we stop.
		AUD_Manager.PlayerDialogue(mSlideStop, gameObject, Player.Value);
	}

	public void HandleSlideReplenish(){
		AUD_Manager.PostEvent("IG_UI_Slide_ST", gameObject);
	}

	private void OnDestroy()
	{
		AUD_Manager.PostEvent("PC_SlideStop_ST", gameObject);
	}
}
