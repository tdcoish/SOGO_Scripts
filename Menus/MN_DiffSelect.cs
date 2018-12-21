using UnityEngine;
using UnityEngine.UI;

public class MN_DiffSelect : MN_Screen
{

	[SerializeField]
	private StringVariable			DifficultyValue;

	// Not used now, could possibly be used later.
	[SerializeField]
	private GameEvent				OnDifficultySelected;

    protected override void MNScreenDisabled()
    {
    }

    protected override void MNScreenEnabled()
    {
    }

	private void Update(){
		base.Update();

		// the text of the button MUST match the text for the difficulty select.
		if(input.aButtonPressed){
			Text btTxt = UT_FindComponent.FindComponent<Text>(mButtons[mActive].gameObject);

			DifficultyValue.Value = btTxt.text;

			MN_Manager.Instance.ShowScreen<MN_LvlSelect>();
			Events.OnButtonPress.Raise(null);
		}

		// If they press b, go back to the main menu.
		if(input.bButtonPressed){
			Events.OnBackScreen.Raise(null);
			MN_Manager.Instance.ShowScreen<MN_CharSelect>();
		}
	}

	// Could also write the buttons this way.
	public void OnHardSelected(){
		DifficultyValue.Value = "HARD";
	}

	public void OnNormalSelected(){
		DifficultyValue.Value = "NORMAL";
	}

	public void OnEasySelected(){
		Debug.Log("Easy");
		DifficultyValue.Value = "EASY";
	}

}
