using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewGameEvent", menuName="SunsOutGunsOut/GameEvent")]
public class GameEvent : ScriptableObject {

	private List<GameEventListener> listeners = new List<GameEventListener>();

	public void Raise(object data) {
		// Go backwards in case we delete one listener
		for (int i = listeners.Count -1; i >= 0; i--) {
			listeners[i].OnEventRaised(data);
		}
	}

	public void RegisterListener(GameEventListener listener) {
		listeners.Add(listener);
	}
	
	public void UnregisterListener(GameEventListener listener) {
		listeners.Remove(listener);
	}
}
