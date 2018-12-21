using UnityEngine;

public class ObjectStatusBar : MonoBehaviour {

    private void Awake() {
		SetScale(0);
    }

    public void SetScale(float newValue) {
		Vector3 newScale = transform.localScale;
		newScale.x = newValue;
        transform.localScale = newScale;
    }
}
