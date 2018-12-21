using UnityEngine;

//Causes object it is attached to to self destruct after a given amount of time.
public class Lifetime : MonoBehaviour {
	[SerializeField]
	[Tooltip("Duration in seconds before object is destroyed.")]
	private float lifetime;

	void Start () {
		Destroy(gameObject, lifetime);
	}
}
