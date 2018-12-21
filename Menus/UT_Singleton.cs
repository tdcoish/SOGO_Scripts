using UnityEngine;

public abstract class UT_Singleton<T> : MonoBehaviour where T : UT_Singleton<T>{

	public static T Instance;
	
	protected void Awake()
	{
		if(Instance == null)
		{
			Instance = GetComponent<T>();
			SingletonAwake();
		}
		else 
		{
			Destroy(this);
		}
	}

	protected abstract void SingletonAwake();
}
