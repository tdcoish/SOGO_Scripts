using UnityEngine;

[CreateAssetMenu(fileName="NewDeflectorProperties", menuName="SunsOutGunsOut/NewDeflectorProperties")]
public class DeflectorProperties : ScriptableObject
{
	public float minDistanceToPerfectDeflection;
	public float maxDistanceToPerfectDeflection;

	public float minHealthIncrease;
	public float maxHealthIncrease;

	public float fixedTimeWindow;
}

