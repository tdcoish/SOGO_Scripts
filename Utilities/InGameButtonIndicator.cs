using UnityEngine;

public class InGameButtonIndicator : MonoBehaviour {

    internal SpriteRenderer indicator;

    private void Awake() {
        indicator = GetComponent<SpriteRenderer>();
        indicator.enabled = false;
    }
}
