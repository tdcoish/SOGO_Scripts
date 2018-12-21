using UnityEngine;
using UnityEngine.SceneManagement;

/*************************************************************************************
Moving within the menu should be done within the same scene, pressing play here should 
just take you to a level selection screen. 
************************************************************************************ */

public class MN_MainMenu : MN_Screen {

	

	#region Screen Life Cycle

		protected override void MNScreenEnabled(){
			// mActive = MN_Manager.Instance.mActiveMainMenuButton;
			Time.timeScale = 1.0f;
			mActive = 0;
		}

		protected override void MNScreenDisabled(){

		}

	#endregion

private void Update(){
		base.Update();
		MN_Manager.Instance.mActiveMainMenuButton = mActive;
	}

	// Our button functions
	// Go to the lobby screen, for now just play my scene
	public void OnPlayPressed(){
		Events.OnButtonPress.Raise(null);		
		// I mean for now, just go to my playground
		// SceneManager.LoadScene("Lvl_BeachArena");
		MN_Manager.Instance.ShowScreen<MN_CharSelect>();
	}

	// Go to Credits Screen
	public void OnCreditsPressed(){
		Events.OnButtonPress.Raise(null);		
		MN_Manager.Instance.ShowScreen<MN_PGGD>();
	}

	// Go to Options Screen
	public void OnOptionsPressed(){
		Events.OnButtonPress.Raise(null);
		MN_Manager.Instance.ShowScreen<MN_Options>();
	}

	// Quit Game, maybe go to end splash screen
	public void OnExitPressed(){
		Events.OnButtonPress.Raise(null);
		MN_Manager.Instance.ShowScreen<MN_Quit>();
	}

}

// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class MainMenuScreen : UIScreen 
// {	
	
// 	#region Screen Life Cycle

// 		protected override void UIScreenEnabled()
// 		{
// 			AudioManager.Instance.PlayEvent("Play_Music_Loop_Menu");
// 		}

// 		protected override void UIScreenDisabled()
// 		{
// 			// Do something on disable
// 		}

// 	#endregion

// 	#region Events
	
// 		public void OnStartGameMenuPressed()
// 		{
// 			SceneManager.LoadScene("Game");
// 			AudioManager.Instance.PlayEvent("Play_Audience_Loop_1");
// 			UIManager.Instance.ShowScreen<GameScreen>();
// 		}

// 		public void OnCreditsMenuPressed()
// 		{	
// 			UIManager.Instance.ShowScreen<CreditsScreen>();
// 		}

// 		public void OnInstructionsMenuPressed()
// 		{
// 			UIManager.Instance.ShowScreen<InstructionsScreen>();
// 		}
	
// 	#endregion

// }