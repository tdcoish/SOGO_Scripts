using UnityEngine;

public class ForceFieldsManager : SingletonBehaviour<ForceFieldsManager>
{
    [SerializeField]
	private EnumVariable[] sections;
	[SerializeField]
	private GameEvent onEnableForceField;
	[SerializeField]
	private GameEvent onMakeForceFieldDamageable;

	protected override void SingletonAwake()
    {
		if (sections.Length > 0) {
			for (int i = 0; i < sections.Length; i++)
			{
				DisableForceField(i);
			}
		}
    }

	public void EnableForceField(int gameSectionIndex) {
		object[] dataset = { sections[gameSectionIndex], true };
		object dataWrapper = dataset;
		onEnableForceField.Raise(dataWrapper);
	}

	public void DisableForceField(int gameSectionIndex) {
		object[] dataset = { sections[gameSectionIndex], false };
		object dataWrapper = dataset;
		onEnableForceField.Raise(dataWrapper);
	}

	public void MakeShieldDamageable(int gameSectionIndex) {
		onMakeForceFieldDamageable.Raise(sections[gameSectionIndex]);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			EnableForceField(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			MakeShieldDamageable(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			EnableForceField(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			MakeShieldDamageable(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5)) {
			EnableForceField(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6)) {
			MakeShieldDamageable(2);
		}
	}
}
