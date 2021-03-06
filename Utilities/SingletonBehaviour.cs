﻿/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
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