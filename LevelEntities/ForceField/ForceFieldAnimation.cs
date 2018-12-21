using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldAnimation : MonoBehaviour {
	
	[SerializeField]
	private float offsetXSpeed = 5f;
	[SerializeField]
	private float offsetYSpeed = 5f;

	private MeshRenderer meshRenderer;
	private Vector2 texOffset;

	private void Awake() {
		meshRenderer = GetComponent<MeshRenderer>();
		texOffset = meshRenderer.material.GetTextureOffset("_MainTex");
	}

	// Update is called once per frame
	void Update () {
		texOffset += new Vector2(offsetXSpeed * Time.deltaTime, offsetYSpeed * Time.deltaTime);		
		meshRenderer.material.SetTextureOffset("_MainTex", texOffset);
	}
}