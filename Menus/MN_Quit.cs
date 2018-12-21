using UnityEngine;
using UnityEngine.SceneManagement;

/*************************************************************************************
This screen displays a message before letting the user truly quit.
************************************************************************************ */

public class MN_Quit : MN_Screen {

	[SerializeField]
	private GameEvent		OnQuitScreenActivated;

	#region Screen Life Cycle

		protected override void MNScreenEnabled(){
			OnQuitScreenActivated.Raise(null);
		}

		protected override void MNScreenDisabled(){

		}

	#endregion

	private void Update(){
		base.Update();
		if(input.bButtonPressed){
			OnKeepPlaying();
		}
	}

	public void OnKeepPlaying(){
		Events.OnBackScreen.Raise(null);
		MN_Manager.Instance.ShowScreen<MN_MainMenu>();
	}

	// Only works for builds, not editor.
	public void OnQuit(){
		SceneManager.LoadScene("MN_Outro");
	}
}
