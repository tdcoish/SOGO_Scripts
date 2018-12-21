// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class MN_UserName : MN_Screen {

// 	public InputField			nameInput;
// 	public Text					showUserName;
// 	public Text					validOrInvalid;

// 	#region Screen Life Cycle

// 		protected override void MNScreenEnabled(){
// 			Debug.Log("Hit enable screen");
// 			validOrInvalid.enabled = false;

// 			// probably shouldn't clear User Name each time, but whatevs
// 			MN_Manager.Instance.userName.Value = "";
// 		}

// 		protected override void MNScreenDisabled(){

// 		}

// 	#endregion

// 	// Go to the lobby screen, for now just play my scene
// 	public void OnEnterName(){
// 		// save username, then go to the lobby screen
		
// 		MN_Manager.Instance.userName.Value = nameInput.text;
// 		Debug.Log(MN_Manager.Instance.userName.Value);

// 		if(nameInput.text != ""){
// 			validOrInvalid.enabled = false;
// 		}

// 		showUserName.text = "Username: " + nameInput.text;

// 	}

// 	public void OnGoToLobbyScreen(){
// 		if(MN_Manager.Instance.userName.Value == ""){
// 			validOrInvalid.enabled = true;
// 			return;
// 		}
// 		MN_Manager.Instance.ShowScreen<MN_Lobby>();
// 	}

// 	public void OnBack(){
// 		MN_Manager.Instance.ShowScreen<MN_MainMenu>();
// 	}

// }
