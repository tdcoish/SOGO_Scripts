using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonBehaviour<EnemyManager>
{
    
	[SerializeField]
	private GameObject redEnemy;
	[SerializeField]
	private GameObject blueEnemy;

	public enum EnemyType { Red, Blue }
	
	protected override void SingletonAwake() {
		
    }

	public void CreateEnemy(EnemyType eType) {
		float randomDistance = Random.Range(-10f, 10f);
		Vector3 position = new Vector3(transform.position.x + randomDistance, transform.position.y + 1, transform.position.z + randomDistance);
		GameObject enemyToSpawn = null;
		switch (eType) {
			case EnemyType.Red: {
				enemyToSpawn = redEnemy;
				break;
			}
			case EnemyType.Blue: {
				enemyToSpawn = blueEnemy;
				break;
			}
		}
		PhotonNetwork.Instantiate(enemyToSpawn.name, position, Quaternion.identity);
	}
}
