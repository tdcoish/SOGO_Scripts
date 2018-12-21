using UnityEngine;

public class SpeedLimiter : MonoBehaviour {

	[SerializeField]
	private float maxSpeed = 20f;

	private Rigidbody rb;

	// Use this for initialization
	private void Awake () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentVelocity = rb.velocity;
		if (currentVelocity.y > maxSpeed) {
			currentVelocity.y = maxSpeed;
			rb.velocity = currentVelocity;
		}
	}
}
