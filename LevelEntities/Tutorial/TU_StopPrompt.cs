using UnityEngine;
using UnityEngine.UI;

public class TU_StopPrompt : MonoBehaviour {

	[SerializeField]
	private GameObject[]			mPrompts;
	[SerializeField]
	private InputState				Input;

	[SerializeField]
	private GameObject				GameScreen;

	private bool 			mActive = false;
	private int 			mInd = 0;

	// All triggers only trigger once.
	private bool 			mHasBeenActivated = false;

	// Use this for initialization
	void Start () {
		for(int i=0; i<mPrompts.Length; i++){
			mPrompts[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!mActive) return;

		Time.timeScale = 0f;

		if(Input.aButtonPressed){
			mPrompts[mInd].SetActive(false);
			mInd++;
			if(mInd >= mPrompts.Length){
				Time.timeScale = 1f;
				mActive = false;
				GameScreen.SetActive(true);
				AUD_Manager.SetState("PauseMenu", "ResumeGame");
				return;
			}
			mPrompts[mInd].SetActive(true);
		}
	}

	// Event is going to be different for all of these.
	public void HandleEvent(){
		if(mHasBeenActivated) return;
		mHasBeenActivated = true;

		AUD_Manager.SetState("PauseMenu", "PauseGame");

		mInd = 0;
		Time.timeScale = 0f;
		mActive = true;
		mPrompts[0].SetActive(true);
		GameScreen.SetActive(false);
	}

}
