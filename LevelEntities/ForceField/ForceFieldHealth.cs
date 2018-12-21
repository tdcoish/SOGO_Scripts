using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldHealth : MonoBehaviour, IDamageable {

	[SerializeField]
	private float health = 100f;

	[SerializeField]
	private Color invincibleHealthColor;
	[SerializeField]
	private Color fullHealthColor;
	[SerializeField]
	private Color lowHealthColor;
	[SerializeField]
	private float forceFieldOpacity = 1f;

	internal bool canTakeDamage = true;
	private MeshRenderer meshRenderer;

	private void Awake() {
		meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.material.SetColor("_TintColor", invincibleHealthColor);
	}

	private void Update() {
		if (canTakeDamage == false) return;
		float normalizedHealth = 1f - (health / 100f);
		meshRenderer.material.SetColor("_TintColor", normalizedHealth.GetNormalizedColor(lowHealthColor, fullHealthColor, forceFieldOpacity));
	}

    public void TakeDamage(float damage)
    {
		if (canTakeDamage) {
			health -= damage;
			if (health <= 0f) {
				Destroy(gameObject);
			}
		}
    }
}
