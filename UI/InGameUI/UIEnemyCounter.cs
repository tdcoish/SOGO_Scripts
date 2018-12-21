using UnityEngine;
using UnityEngine.UI;

public class UIEnemyCounter : MonoBehaviour {

	[SerializeField]
	private IntegerVariable enemyCount;

	[SerializeField]
	private Text counterText;
	[SerializeField]
	private Text alienLabel;
	
	// Update is called once per frame
	void Update () {
		counterText.text = enemyCount.Value.ToString();
		alienLabel.text = "Aliens Alive";
		if (enemyCount.Value == 1) {
			alienLabel.text = "Alien Alive";
		}
	}
}
