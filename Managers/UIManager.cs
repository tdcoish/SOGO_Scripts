/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/

using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
	private UIScreen currentScreen;
	private Dictionary<Type, UIScreen> screens = new Dictionary<Type, UIScreen>();
    
	protected override void SingletonAwake()
    {
        foreach(UIScreen screen in GetComponentsInChildren<UIScreen>(true))
		{
			screen.gameObject.SetActive(false);
			screens.Add(screen.GetType(), screen);
		}
		ShowScreen<GameScreen>(); // Set Default
    }

	public void ShowScreen<T>() where T : UIScreen
	{
		if(currentScreen != null)
		{
			currentScreen.gameObject.SetActive(false);
		}

		if(screens.ContainsKey(typeof(T)) == false)
		{
			return;
		}

		currentScreen = screens[typeof(T)];
		currentScreen.gameObject.SetActive(true);
	}

	public T GetScreen<T>() where T : UIScreen
	{
		return screens[typeof(T)] as T;
	}
}