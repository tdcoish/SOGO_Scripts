using UnityEngine;

/****************************************************************
Listens to sections being done game events, then plays this sound. 

We have to wait a little bit now so that the audience doesn't have to suffer
through that line every time, unless they are just dicking around.
************************************************************** */
public class AD_AnnounceSection : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue			mSectionDone;

	[SerializeField]
	private SO_AD_Dialogue			mLevelStart;

	[SerializeField]
	private float			mDelay = 8f;
	private bool 			mWaitingForPlayer = false;
	private float 			mSectionDoneTimeStamp;

	private void Update(){
		if(!mWaitingForPlayer) return;

		if(Time.time - mSectionDoneTimeStamp > mDelay){
			AUD_Manager.DynamicDialogueFromData(mSectionDone, gameObject);
			mSectionDoneTimeStamp = Time.time;
		}
	}

	// hack, but we're also setting state to not paused.
	public void PlayLevelStartLine(){
		AUD_Manager.SetState("PauseMenu", "ResumeGame");
		AUD_Manager.DynamicDialogueFromData(mLevelStart, gameObject);
	}

	public void OnSectionDone(){
		mWaitingForPlayer = true;
		mSectionDoneTimeStamp = Time.time;
	}
	
	public void OnSectionStarted(){
		mWaitingForPlayer = false;
	}
}
