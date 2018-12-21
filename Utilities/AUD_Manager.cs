using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AUD_Manager {

	public static uint PostEvent(string eventName, GameObject go)
	{
		return AkSoundEngine.PostEvent(eventName, go);
	}

	public static void PostEvent(AK.Wwise.Event wwEvent, GameObject go)
	{
		wwEvent.Post(go);
	}

	public static void SetSwitch(string switchGroup, string switchState, GameObject go)
	{
		AkSoundEngine.SetSwitch(switchGroup, switchState, go);
	}

	public static void SetSwitch(AK.Wwise.Switch wwSwitch, GameObject go)
	{
		wwSwitch.SetValue(go);
	}

	public static void EventFromData(SO_AD_Sound data, GameObject go){
		for(int i=0; i<data.mSwitchGroups.Length; i++){
			SetSwitch(data.mSwitchGroups[i], data.mSwitchStates[i], go);
		}
		PostEvent(data.mEvent, go);
	}

	
	// Could pass in "none" as a state, 
	public static void SetState(string stateGroup, string state){
		AkSoundEngine.SetState(stateGroup, state);
	}

	// This works because all the dialogue is jon.thing or dom.thing
	public static void PlayerDialogue(SO_AD_Dialogue data, GameObject go, string player){
		string[] args = data.mArgs;
		args[0] = player;
		DynamicDialogue(data.mEventName, args, go);
	}

	public static void DynamicDialogueFromData(SO_AD_Dialogue data, GameObject go){
		DynamicDialogue(data.mEventName, data.mArgs, go);
	}

	public static void DynamicDialogue(string eventName, string[] args, GameObject go){

		// Generated locally, no match in project
		uint sequenceID = AkSoundEngine.DynamicSequenceOpen(go);
		// eg. VO_Positives_E
		uint eventUINT = AkSoundEngine.GetIDFromString(eventName);

		// build hashes for arguments, eg. Grunt, Cheer,
		uint[] argUINTS = new uint[args.Length];
		for(int i=0; i<argUINTS.Length; i++){
			argUINTS[i] = AkSoundEngine.GetIDFromString(args[i]);
		}
		// audio file hash
		uint nodeID = AkSoundEngine.ResolveDialogueEvent(eventUINT, argUINTS, (uint)argUINTS.Length);

		// sequence of events being played.
		AkPlaylist playList = AkSoundEngine.DynamicSequenceLockPlaylist(sequenceID);
		playList.Enqueue(nodeID);

		AkSoundEngine.DynamicSequenceUnlockPlaylist(sequenceID);

		AkSoundEngine.DynamicSequencePlay(sequenceID);

		AkSoundEngine.DynamicSequenceClose(sequenceID);
	}
	// string[] args = new string[numArgs]{
	// 	"Grunt", "Cheer"
	// };
	// AUD_Manager.DynamicDialogue("VO_Positives_E", args, this);

}
