using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
	[SerializeField]
	private static int initialPoolSize = 5;

	private static Dictionary<int, List<PoolableObject>> objectPool = new Dictionary<int, List<PoolableObject>>();
	private static Dictionary<int, int> poolIndexes = new Dictionary<int, int>();


	private static PoolableObject AddObjectToPool(PoolableObject poPrefab)
	{
		int id = poPrefab.GetInstanceID();
		var clone = GameObject.Instantiate(poPrefab, Vector3.zero, Quaternion.identity);
		clone.gameObject.SetActive(false);
		objectPool[id].Add(clone);
		return clone; 
	}

	public static void CreatePool(PoolableObject poPrefab, int size)
	{
		int id = poPrefab.GetInstanceID();
		objectPool[id] = new List<PoolableObject>(size);
		poolIndexes[id] = 0;
		for(int i = 0;i < size;++i)
		{
			AddObjectToPool(poPrefab);
		}
	}

	public static PoolableObject GetObjectFromPool(PoolableObject prefab)
	{
		int id = prefab.GetInstanceID();
		if(!objectPool.ContainsKey(id))
		{	
			CreatePool(prefab, initialPoolSize);
		}
		
		for(int i = 0;i < objectPool[id].Count; i++)
		{
			poolIndexes[id] = (poolIndexes[id] + 1) % objectPool[id].Count; // make sthe iterator go back to position 0
			if(objectPool[id][poolIndexes[id]].gameObject.activeInHierarchy == false)
			{
				return objectPool[id][poolIndexes[id]];
			}
		}


		return AddObjectToPool(prefab);
	}
}
