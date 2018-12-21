using UnityEngine;

public class ScoreManager : MonoBehaviour {

	[SerializeField]
	private GameEvent addScore;
	[SerializeField]
	private FloatVariable score;
	private float lastCheckpointScore = 0f;

	private void Awake() {
		ResetScore(null);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.P)) {
			addScore.Raise(1000f);
		}
	}

	public void ResetScore(object data) {
		score.Value = 0;
		lastCheckpointScore = 0;
	}

	public void AddScore(object data) {
		score.Value += (float) data;
	}

	public void SaveLastScore(object data) {
		lastCheckpointScore = score.Value;
	}

	public void SetLastScore(object data) {
		score.Value = lastCheckpointScore;
	}
}
