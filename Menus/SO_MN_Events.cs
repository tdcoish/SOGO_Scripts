using UnityEngine;

[CreateAssetMenu(fileName="MenuEvents", menuName="SunsOutGunsOut/MN/MenuEvents")]
public class SO_MN_Events : ScriptableObject {

	public GameEvent		OnBackScreen;
	public GameEvent		OnButtonPress;
	public GameEvent		OnTransition;
	public GameEvent		OnScrollUp;
	public GameEvent 		OnScrollDown;
}
