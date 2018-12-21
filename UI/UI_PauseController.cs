using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseController : MonoBehaviour {

	[SerializeField]
	private UI_PauseMenu		mPauseMenu;

	[SerializeField]
	private InputState input;

	// TODO: USE SCRITABLE OBJECT
	[SerializeField]
	private BooleanVariable isGamePaused;

	// [SerializeField]
	// private FreeCameraController	mCamera;

	private bool 			mIsPaused = false;

	private void Update(){
		if(input.startButtonPressed){
			// do thing.
			mIsPaused = !mIsPaused;
		}
		// control time based on whether or not we're supposed to be paused.
		if(mIsPaused){
			Time.timeScale = 0f;
			// mCamera._FreezeEverything = true;
			mPauseMenu.gameObject.SetActive(true);
		}else{
			Time.timeScale = 1f;
			Debug.Log("UI_PauseController just unfroze time");
			// mCamera._FreezeEverything = false;
			mPauseMenu.gameObject.SetActive(false);
		}
	}
}
