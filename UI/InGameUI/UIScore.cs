using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour {

	[SerializeField]
	private bool displayLabel = false;
	[SerializeField]
	private FloatVariable score;
	[SerializeField]
	private Text scoreText;

	private void Update () {
		string scoreStr = Mathf.FloorToInt(score.Value).ToString();
		scoreText.text = displayLabel ? "Score: " + scoreStr : scoreStr;
	}
}
