using UnityEngine;

// Char Select -> Difficulty Select -> Level Select
public class MN_CharSelect : MN_Screen
{
	[SerializeField]
	private StringVariable 		CharSelect;

	[SerializeField]
	private SO_AD_Dialogue		mGratitudeLine;

	[SerializeField]
	private MN_SnapCamera		mCam;

    protected override void MNScreenDisabled()
    {

    }

    protected override void MNScreenEnabled()
    {

    }

	private void Update(){
		base.Update();

		if(input.bButtonPressed){
			Events.OnBackScreen.Raise(null);
			MN_Manager.Instance.ShowScreen<MN_MainMenu>();
		}
	}

	public void OnDomSelected(){
		CharSelect.Value = "dom";
		MN_Manager.Instance.ShowScreen<MN_DiffSelect>();
		AUD_Manager.PlayerDialogue(mGratitudeLine, mCam.gameObject, CharSelect.Value);
		Events.OnButtonPress.Raise(null);
	}

	public void OnJonSelected(){
		CharSelect.Value = "jon";
		MN_Manager.Instance.ShowScreen<MN_DiffSelect>();
		AUD_Manager.PlayerDialogue(mGratitudeLine, mCam.gameObject, CharSelect.Value);
		Events.OnButtonPress.Raise(null);
	}

}
