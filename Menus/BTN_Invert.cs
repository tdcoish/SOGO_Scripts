using UnityEngine;
using UnityEngine.UI;

public class BTN_Invert : BTN_Options {

	[SerializeField]
	private InputState			InputState;

	[SerializeField]
	private Text 				YesOrNoText;

	private void Awake(){
		SetText();
	}

	public void OnControlsInverted(){
		InputState.invertXAxis = !InputState.invertXAxis;
		// InvertControls.Value = !InvertControls.Value;
		SetText();
	}

	private void SetText(){
		if(InputState.invertXAxis== true){
			YesOrNoText.text = "Yes";
		}else{
			YesOrNoText.text = "No";
		}
	}

	public override void OnPressLeft(){
		base.OnPressLeft();
		OnControlsInverted();
	}

	public override void OnPressRight(){
		base.OnPressRight();
		OnControlsInverted();
	}
}
