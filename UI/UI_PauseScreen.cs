using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseScreen : MonoBehaviour {

	[SerializeField]
	private InputState	input;

	private bool paused = false;

	[SerializeField]
	private GameObject PauseMenu;

	[SerializeField]
	private GameObject DeathMenu;

	[SerializeField]
	private GameEvent 	PAUSED_EVENT;

	[SerializeField]
	private GameEvent	UNPAUSED_EVENT;

	private float 		tmLastPress;
	private float		minTmBetweenPresses = 0.1f;

	private void Awake(){
		DeathMenu.SetActive(false);
	}

	private void Update(){
		if(Time.time - tmLastPress > minTmBetweenPresses){
			if(input.startButtonPressed){
				tmLastPress = Time.time;

				if(!paused){
					PAUSED_EVENT.Raise(null);
				}else{
					UNPAUSED_EVENT.Raise(null);
				}
			}
		}
	}

	// Here we handle the event that we have paused.
	public void OnPaused(){
		paused = true;
		PauseMenu.SetActive(true);
		Time.timeScale = 0f;
	}

	public void OnUnpaused(){
		paused = false;
		PauseMenu.SetActive(false);
		Time.timeScale = 1f;
	}

	public void UI_PlayerDied(object data){
		DeathMenu.SetActive(true);

		//Time.timeScale = 0.8f;


		// Make camera 
	}
}
