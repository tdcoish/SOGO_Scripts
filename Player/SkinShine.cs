using UnityEngine;

public class SkinShine : MonoBehaviour {

	[SerializeField]	
	private FloatVariable playerHealth;

	private Material[] playerMaterials;

	private void Awake() {
		SkinnedMeshRenderer[] meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
		playerMaterials = new Material[meshes.Length];
		for (int i = 0; i < meshes.Length; i++) {
			playerMaterials[i] = meshes[i].material;
		}
	}
	
	private void Update () {
		foreach (Material mat in playerMaterials)
		{
			float normalizedValue = playerHealth.Value.Normalize(0f, 100f);
			// mat.SetFloat("_GlossyReflections", normalizedValue);
		}
	}
}
