using UnityEngine;
using UnityEngine.UI;

public class UI_ComboCounter : MonoBehaviour {

	[SerializeField]
	private IntegerVariable comboCounter;
	private Text counter;

	private void Awake() {
		counter = GetComponent<Text>();	
	}

	private void Update() {
		counter.text = "Perfect Deflections Combo: <b>" + comboCounter.Value + "</b>";
	}

}
