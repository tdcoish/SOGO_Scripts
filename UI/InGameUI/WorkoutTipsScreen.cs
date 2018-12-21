using UnityEngine;
using UnityEngine.UI;

public class WorkoutTipsScreen : UIScreen {

	[SerializeField]
	private Sprite[] tips;
	[SerializeField]
	private float timeBetweenChanges = 0.3f;
	[SerializeField]
	private Image currentTip;

	private int currentTipIndex;
	private float lastChangeTimestamp = 0;
    
	protected override void UIScreenDisabled()
    {
		AUD_Manager.PostEvent("FE_UI_Back_ST", gameObject);
    }

    protected override void UIScreenEnabled()
    {
        currentTipIndex = 0;
		currentTip.sprite = tips[currentTipIndex];
		StartCoroutine(SetPreselectedObject());
		AUD_Manager.PostEvent("FE_UI_Enter_ST", gameObject);
    }

	private void Update() {
		if (input.bButtonPressed) {
			OnBackButtonClicked();
		}
		HandleWorkoutTipSwitch();
	}

	private void HandleWorkoutTipSwitch() {
		if ((input.leftStickX >= 0.5f || input.leftStickX <= -0.5f) && Time.unscaledTime >= lastChangeTimestamp + timeBetweenChanges) {
			lastChangeTimestamp = Time.unscaledTime;
			if (input.leftStickX >= 0.5f) {
				if (currentTipIndex == tips.Length - 1) currentTipIndex = 0;
				else currentTipIndex++;
			} else if (input.leftStickX <= 0.5f) {
				if (currentTipIndex == 0) currentTipIndex = tips.Length - 1;
				else currentTipIndex--;
			}
			currentTip.sprite = tips[currentTipIndex];
			AUD_Manager.PostEvent("FE_Transitions_ST", gameObject);
		}
	}

	public void OnBackButtonClicked() {
		UIManager.Instance.ShowScreen<PauseScreen>();
	}

}
