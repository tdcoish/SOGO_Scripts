using System;
using System.Collections.Generic;
using UnityEngine;

public class MN_Manager : UT_Singleton<MN_Manager> {
	
	private MN_Screen currentScreen;
	private Dictionary<Type, MN_Screen> screens = new Dictionary<Type, MN_Screen>();

	// Set by the main menu constantly.
	public int 		mActiveMainMenuButton = 0;

	[SerializeField]
	private GameEvent			OnScreenTransition;

	[HideInInspector]
	public string 				mSceneToLoad;

	protected override void SingletonAwake()
    {
        foreach(MN_Screen screen in GetComponentsInChildren<MN_Screen>(true))
		{
			screen.gameObject.SetActive(false);
			screens.Add(screen.GetType(), screen);
		}
		ShowScreen<MN_MainMenu>(); // Set Default
    }

	public void ShowScreen<T>() where T : MN_Screen
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

		OnScreenTransition.Raise(null);
	}

	public T GetScreen<T>() where T : MN_Screen
	{
		return screens[typeof(T)] as T;
	}

}



