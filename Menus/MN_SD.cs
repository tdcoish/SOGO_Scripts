using UnityEngine;
using UnityEngine.UI;

/**************************************************************************************************
Credits screen now has a bunch of features, such as gamma control, control inversion, Volume, Music ON/OFF.
************************************************************************************************ */
public class MN_SD : MN_Credits {

	protected void Update(){
		base.Update();

		if(input.bButtonPressed){
			Events.OnBackScreen.Raise(null);
			MN_Manager.Instance.ShowScreen<MN_MainMenu>();
		}
	}

    protected override void PageUp(){
        MN_Manager.Instance.ShowScreen<MN_PGGD>();
    }

    protected override void PageDown(){
        MN_Manager.Instance.ShowScreen<MN_Actors>();
    }

}
