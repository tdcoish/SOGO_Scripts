using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : UIScreen
{
	
	[Header("Options Variables")]
	[SerializeField]
	private InputState input;
	[SerializeField]
	private BooleanVariable musicOnOff;
	[SerializeField]
	private FloatVariable gammaStatus;
	[SerializeField]
	private FloatVariable volumeStatus;

	[Space, Header("UI Elements")]
	[SerializeField]
	private Toggle invertYAxisToggle; 
	[SerializeField]
	private Toggle invertXAxisToggle; 
	[SerializeField]
	private Toggle volumeToggle; 
	[SerializeField]
	private Slider volumeSlider; 
	[SerializeField]
	private Slider gammaSlider; 

	protected override void UIScreenDisabled()
    {
        AUD_Manager.PostEvent("FE_UI_Back_ST", gameObject);
    }

    protected override void UIScreenEnabled()
    {
        StartCoroutine(SetPreselectedObject());
		invertYAxisToggle.isOn = input.invertYAxis;
		invertXAxisToggle.isOn = input.invertXAxis;
		volumeToggle.isOn = musicOnOff.Value;
		volumeSlider.value = volumeStatus.Value / 100;
		gammaSlider.value = gammaStatus.Value;
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

	public void OnInvertYAxisSelected(bool newValue) {
		input.invertYAxis = newValue;
		invertYAxisToggle.isOn = input.invertYAxis;
	}
	
	public void OnInvertXAxisSelected(bool newValue) {
		input.invertXAxis = newValue;
		invertXAxisToggle.isOn = input.invertXAxis;
	}
	
	public void OnToggleMusicSelectec(bool newValue) {
		musicOnOff.Value = newValue;
		volumeToggle.isOn = musicOnOff.Value;
		if (musicOnOff.Value) {
			AUD_Manager.SetState("MuteState", "Off");
		}else{
			AUD_Manager.SetState("MuteState", "On");
		}
	}

	public void OnGamaSliderChange(float newValue) {
		gammaStatus.Value = newValue;
		gammaSlider.value = gammaStatus.Value;
	}
	
	public void OnVolumeSliderChange(float newValue) {
		volumeStatus.Value = newValue * 100;
		volumeSlider.value = newValue;
		AkSoundEngine.SetRTPCValue("Master_Level", volumeStatus.Value);
	}
}
