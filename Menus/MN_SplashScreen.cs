using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*****************************************************************
Script basically just exists to load the main menu asynchronously
*************************************************************** */
public class MN_SplashScreen : MonoBehaviour {

	bool hasStartedLoading = false;

	private void Update(){
		if(!hasStartedLoading){
			hasStartedLoading = true;
			StartCoroutine(LoadMainMenuAsync());
		}
		// if(Input.GetMouseButtonDown(0)){
		// 	StartCoroutine(LoadMainMenuAsync());

		// }
	}

	// Loads main menu in the background while the current scene runs.
	IEnumerator LoadMainMenuAsync(){
		
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MN_TestMain");

		while(!asyncLoad.isDone){
			yield return null;
		}

	}
}
