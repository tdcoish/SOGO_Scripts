using UnityEngine;

[CreateAssetMenu(fileName="SO_LVL_SectionEvents", menuName="LVL/SectionEvents")]
public class SO_LVL_LoadSectionEvents : ScriptableObject {

    public int                  indice;
    public GameEvent[]          Events;

    public void TriggerActiveEvent(){
        Events[indice].Raise(null);
    }
}