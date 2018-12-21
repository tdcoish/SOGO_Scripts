using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

	void Start(){
		Cursor.lockState = CursorLockMode.None;
	}
	
	//manually go to the game scene we want.
	public void OnPressedStartGame(){

		// SceneManager.LoadScene("stayAlive");
		SceneManager.LoadScene("FPS_Training");
	}
}
