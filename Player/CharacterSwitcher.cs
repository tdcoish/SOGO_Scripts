using UnityEngine;

public class CharacterSwitcher : MonoBehaviour {

	[SerializeField]
	private StringVariable selectedCharacter;
	[SerializeField]
	private SkinnedMeshRenderer smr;

	[Space]
	[Header("Jon Assets")]
	[SerializeField]
	private Mesh jonMesh;
	[SerializeField]
	private Material jonMaterial;
	[Space]
	[Header("Dom Assets")]
	[SerializeField]
	private Mesh domMesh;
	[SerializeField]
	private Material domMaterial;

	// Use this for initialization
	private void Awake () {
		smr = GetComponentInChildren<SkinnedMeshRenderer>();
		if (selectedCharacter.Value == "jon") {
			smr.sharedMesh = Instantiate(jonMesh);
			smr.material = jonMaterial;
		} else if(selectedCharacter.Value == "dom") {
			smr.sharedMesh = Instantiate(domMesh);	
			smr.material = domMaterial;
		}
	}
}
