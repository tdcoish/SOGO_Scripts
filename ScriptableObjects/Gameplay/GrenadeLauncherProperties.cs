using UnityEngine;

[CreateAssetMenu(fileName="NewGrenadeLauncherProperties", menuName="SunsOutGunsOut/NewGrenadeLauncherProperties")]
public class GrenadeLauncherProperties : ScriptableObject
{
    public float throwingAngle;// = 10f
    public float throwingIncreaseSpeed;// = 25f;
	public float minThrowingForce;
	public float maxThrowingForce;
    public float coolDownTime; 
}

