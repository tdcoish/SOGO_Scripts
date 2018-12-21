/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/
using UnityEngine;

[CreateAssetMenu(fileName="NewBulletProperties", menuName="SunsOutGunsOut/NewBulletProperties")]
public class BulletProperties : ScriptableObject
{
	
	public float lifeSpan = 10f;
	[Space]
	public float defaultSpeed = 20f;
	public float easySpeedMult = 0.5f;
	public float normalSpeedMult = 0.75f;
	public float maxRandomSpeed = 30f;
	public float maxPerfectSpeed = 60f;
	[Space]
	public float defaultDamage = 25f;
	public float easyDamageMult = 0.2f;
	public float normalDamageMult = 0.5f;
	public float maxPerfectDamage = 100f;
	[Space]
	public float maxYAngleRandomLimit = 45f;
	public float maxXAngleRandomLimit = 45f;
	[Space]
	public float minYAnglePerfectLimit = 10f;
	public float maxYAnglePerfectLimit = 30f;
	public float minXAnglePerfectLimit = 10f;
	public float maxXAnglePerfectLimit = 30f;
	[Space]
	public bool startHomingInmediatly = true;
	public float stopHomingLimit = 0f;
	public float enemyShotHomingTurnSpeed = 300f;
	public float minHomingTurnSpeed = 500f;
	public float maxHomingTurnSpeed = 800f;
	[Space]
	public string ad_loop;
	public string ad_stop;
}
