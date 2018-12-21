using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModelSwaper : MonoBehaviour {

	[SerializeField]
	private StringVariable selectedCharacter;
	[SerializeField]
	private GameObject jonRig;
	[SerializeField]
	private GameObject domRig;
	
	// Use this for initialization
	void Awake () {
		if (selectedCharacter.Value == "jon") {
			jonRig.SetActive(true);
			domRig.SetActive(false);
		} else if (selectedCharacter.Value == "dom") {
			jonRig.SetActive(false);
			domRig.SetActive(true);
		}
	}
}
