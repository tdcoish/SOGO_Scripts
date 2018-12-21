using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

// For now just a tag, eventually will have functionality of in game pause menu.
public class UI_PauseMenu : MonoBehaviour {

	[SerializeField]
	private InputState	input;

	[SerializeField]
	private GameEvent	OnLevelQuit;

	private void Update(){
		if(input.bButtonPressed){
			Time.timeScale = 1f;
			OnLevelQuit.Raise(null);
			SceneManager.LoadSceneAsync("MN_TestMain");
		}
	}
}


// // For now just a tag, eventually will have functionality of in game pause menu.
// public class UI_PauseMenu : MonoBehaviour {

// 	public UnityEngine.UI.InputField			brightnessInput;
// 	private float								mBrightSettings;
// 	// RenderSettings.ambientLight = Color.Lerp(ambientDarkest, ambientLightest, sliderValue);


// 	public void OnEnterBrightness(){
// 		mBrightSettings = float.Parse(brightnessInput.text);
// 		mBrightSettings = Mathf.Clamp(mBrightSettings, -10f, 10f);

// 		PostProcessingBehaviour ppB = Camera.main.GetComponent<PostProcessingBehaviour>();
// 		ColorGradingModel.Settings sets = new ColorGradingModel.Settings();
// 		sets = ppB.profile.colorGrading.settings;
// 		sets.basic.postExposure = mBrightSettings;
// 		ppB.profile.colorGrading.settings = sets;

// 		Debug.Log("New ambient settings");
// 		// RenderSettings.ambientLight = new Color(mBrightSettings, mBrightSettings, mBrightSettings, 1);
// 	}
   
// }
