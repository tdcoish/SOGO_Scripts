// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class MN_Lobby : MN_Screen {

// 	public Text			showUserName;

// 	[SerializeField]
// 	private Button		joinButton;

// 	[SerializeField]
// 	private Button		playButton;

// 	#region Screen Life Cycle

// 		protected override void MNScreenEnabled(){
// 			showUserName.text = "Welcome: " + MN_Manager.Instance.userName.Value;

// 			// when we start up with this screen, we want the user unable to hit join until they have selected a valid lobby
// 			joinButton.interactable = false;
// 			// we can't let them play unless they have joined a lobby.
// 			// except not actually, since there's no system really in place yet.
// 			// playButton.interactable = false;
// 		}

// 		protected override void MNScreenDisabled(){

// 		}

// 	#endregion

// 	// Go to the lobby screen, for now just play my scene
// 	public void OnStartGame(){
// 		// I mean for now, just go to my playground
// 		SceneManager.LoadScene("TimPlayground");
// 	}
// 	public void OnBack(){
// 		MN_Manager.Instance.ShowScreen<MN_MainMenu>();
// 	}

// }
