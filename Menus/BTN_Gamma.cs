using UnityEngine;
using UnityEngine.UI;
using System;

public class BTN_Gamma : BTN_Options {

	[SerializeField]
	private FloatVariable		Gamma;
	[SerializeField]
	private Text 				Amount;

	private void Awake(){
		SetText();
	}

	private void SetText(){
		Amount.text = Gamma.Value.ToString("n2");
	}

	public override void OnPressLeft(){
		base.OnPressLeft();
		Gamma.Value -= 0.1f;
		NormalizeGamma();
		SetText();
	}

	public override void OnPressRight(){
		base.OnPressRight();
		Gamma.Value += 0.1f;
		NormalizeGamma();
		SetText();
	}

	private void NormalizeGamma(){
		if(Gamma.Value < 0f){
			Gamma.Value = 0f;
		}
		if(Gamma.Value > 1f){
			Gamma.Value = 1f;
		}
	}
}
