/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnStart : MonoBehaviour 
{
	[SerializeField]
	public bool loadDebugScene = false;
	[SerializeField]
	private string menusScene;
	[SerializeField]
	public string mainGameScene;
	[SerializeField]
	public string debugScene;

	private void Start()
	{
		menusScene = menusScene.Trim();
		if(menusScene.Length > 0)
		{
			SceneManager.LoadScene(menusScene);
		}
	}
}