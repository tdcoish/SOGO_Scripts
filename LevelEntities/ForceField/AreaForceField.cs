using UnityEngine;

public class AreaForceField : MonoBehaviour {

	[SerializeField]
	private EnumVariable section;

	private ForceFieldHealth health;
	private MeshRenderer meshRenderer;
	private Collider coll;

	private void Awake() {
		coll = GetComponent<Collider>();
		meshRenderer = GetComponent<MeshRenderer>();
		health = GetComponent<ForceFieldHealth>();
		health.canTakeDamage = false;
	}

	public void OnMakeShieldDamageable(object data) {
		if (section == (EnumVariable) data) {
			health.canTakeDamage = true;
		}
	}

	public void OnEnableForceField(object data) {
		object[] dataset = (object[]) data;
		if (section == (EnumVariable) dataset[0]) {
			coll.enabled = (bool) dataset[1];
			meshRenderer.enabled = (bool) dataset[1];
		}
	}
}
