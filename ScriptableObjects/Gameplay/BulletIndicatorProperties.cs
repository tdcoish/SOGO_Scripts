/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/
using UnityEngine;

[CreateAssetMenu(fileName="NewBulletIndicatorProperties", menuName="SunsOutGunsOut/BulletIndicatorProperties")]
public class BulletIndicatorProperties : ScriptableObject
{
	public float minDistanceRange;
	
	[Space]
	public float incommingBulletRingMaxSize;
	public float outerRingSize;
	public float innerRingSize;
}
