using UnityEngine;

public enum SpawnTypes : int {
    RED,
    BLUE,
    MIXED
}

[CreateAssetMenu(fileName="NewAISpawner", menuName="SunsOutGunsOut/AI/SpawnerProperties")]
public class SO_AI_Spawner : ScriptableObject
{
    // NOTE: Individual spawners don't know how many they should be spawning. That is controlled by AI_Master.
    [Header("Rate to spawn at")]
    public float            mSpawnRate;

}
