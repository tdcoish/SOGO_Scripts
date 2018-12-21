using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScreen : UIScreen {

    protected override void UIScreenDisabled()
    {
        AUD_Manager.PostEvent("FE_UI_Back_ST", gameObject);
    }

    protected override void UIScreenEnabled()
    {
		AUD_Manager.PostEvent("FE_UI_Enter_ST", gameObject);
    }

	private void Update() {
		if (input.bButtonPressed) {
			OnBackButtonClicked();
		}
	}

	public void OnBackButtonClicked() {
		UIManager.Instance.ShowScreen<PauseScreen>();
	}
}
