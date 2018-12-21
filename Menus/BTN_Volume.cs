using UnityEngine;
using UnityEngine.UI;
using System;

public class BTN_Volume : BTN_Options {

	[SerializeField]
	private FloatVariable		Volume;
	[SerializeField]
	private Text 				Amount;

	private void Start(){
		SetText();
	}

	private void SetText(){
		Amount.text = Volume.Value.ToString("n0");
	}

	public override void OnPressLeft(){
        base.OnPressLeft();
		Volume.Value -= 10f;
		NormalizeVolume();
		SetText();
	}

	public override void OnPressRight(){
        base.OnPressRight();
		Volume.Value += 10f;
		NormalizeVolume();
		SetText();
	}

	private void NormalizeVolume(){
		if(Volume.Value < 0f){
			Volume.Value = 0f;
		}
		if(Volume.Value > 100f){
			Volume.Value = 100f;
		}
		AkSoundEngine.SetRTPCValue("Master_Level", Volume.Value);
	}
}
