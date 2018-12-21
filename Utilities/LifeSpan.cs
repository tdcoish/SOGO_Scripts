using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : MonoBehaviour {

	[SerializeField]
	private float _LifeSpan=5;

	private void Start() {
		Destroy(gameObject, _LifeSpan);
	}
}
