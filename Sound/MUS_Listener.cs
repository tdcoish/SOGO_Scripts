using UnityEngine;
using UnityEngine.UI;

/**************************************************************************
This gets shoved into the scene and listens for various game events, such as tutorial
finished. It's entire purpose is to play different music tracks.
************************************************************************ */
public class MUS_Listener : MonoBehaviour {

	[SerializeField]
	private string 			mObjectiveSwitch;

	[SerializeField]
	private string 			mObjectiveState;

	[SerializeField]
	private string 			mGameplayMusicPlay;

	[SerializeField]
	private string 			mGameplayMusicStop;

	[SerializeField]
	private string 			mFirstObjState;

	[SerializeField]
	private Text 			mIntensityText;

	
	private void Start(){
		AUD_Manager.SetSwitch("Objective", mObjectiveState, gameObject);
		AUD_Manager.SetSwitch("ObjectiveProgress", "Low", gameObject);
		AUD_Manager.PostEvent(mGameplayMusicPlay, gameObject);

		mIntensityText.text = "Music Intensity: " + "Low";
	}

	// Just for now, we test
	private void Update(){
		if(Input.GetMouseButtonDown(0)){
			StopTutorialMusic();
		}
	}

	public void StopTutorialMusic(){
		// AUD_Manager.PostEvent(mGameplayMusicStop, gameObject);
		// AUD_Manager.SetState(mObjectiveSwitch, mFirstObjState);

		// AUD_Manager.PostEvent(mGameplayMusicStop, gameObject);
		AUD_Manager.SetSwitch(mObjectiveSwitch, mFirstObjState, gameObject);

		mIntensityText.text = "Music Intensity: " + mFirstObjState;


		// AUD_Manager.PostEvent(mGameplayMusicPlay, gameObject);
	}
}
