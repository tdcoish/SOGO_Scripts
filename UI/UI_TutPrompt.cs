using UnityEngine;
using UnityEngine.UI;

public class UI_TutPrompt : MonoBehaviour {
	
	private bool 		mActive = false;
	private bool 		mHasBeenHit = false;

	[SerializeField]
	private InputState		input;

	[SerializeField]
	private UI_TutPromptChild 		mCanvas;

	private void Awake(){
		// mCanvas = GetComponentInChildren<UI_TutPromptChild>();
	}

	private void Update(){
		if(!mActive) return;
		Time.timeScale = 0f;

		if(input.aButtonPressed){
			Time.timeScale = 1f;
			mCanvas.gameObject.SetActive(false);
			mActive = false;
		}
	}

	public void HandleTriggerHit(){
		if(mHasBeenHit) return;
		
		Time.timeScale = 0f;
		mActive = true;
		mHasBeenHit = true;
		mCanvas.gameObject.SetActive(true);
	}

}
