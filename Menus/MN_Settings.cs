using UnityEngine;

public class MN_Settings : MN_Screen
{

	#region Screen Life Cycle

		protected override void MNScreenEnabled(){

		}

		protected override void MNScreenDisabled(){

		}

	#endregion

	public void OnBack(){
		// get the menu manager
		MN_Manager pageController = FindObjectOfType<MN_Manager>();
		if(!pageController){
			Debug.Log("No page controller in this scene. Very weird.");
			return;
		}

		Debug.Log("Was manager");
		pageController.ShowScreen<MN_MainMenu>();
	}
}
