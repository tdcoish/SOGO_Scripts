using UnityEngine;
using UnityEngine.UI;

public class BTN_Music : BTN_Options {

	[SerializeField]
	private BooleanVariable		MusicEnabled;
	[SerializeField]
	private Text 				YesOrNoText;

	private void Start(){
		SetText();
	}

	public void OnMusicToggled(){
		MusicEnabled.Value = !MusicEnabled.Value;
		if(MusicEnabled.Value){
			AUD_Manager.SetState("MuteState", "Off");
		}else{
			AUD_Manager.SetState("MuteState", "On");
		}
		SetText();
	}

	private void SetText(){
		if(MusicEnabled.Value == true){
			YesOrNoText.text = "On";
		}else{
			YesOrNoText.text = "Off";
		}
	}

	public override void OnPressLeft(){
		base.OnPressLeft();
		OnMusicToggled();
	}

	public override void OnPressRight(){
		base.OnPressRight();
		OnMusicToggled();
	}
}
