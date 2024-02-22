using UnityEngine;


[CreateAssetMenu(menuName = "Random Wave",
    fileName = "Random Wave", order = 0)]

public class randomWaveSpawnerWaves : ScriptableObject
{
    public int waveLength = 10;
    public GameObject[] avaliableEnemies;
    public int[] avaliableEnemyCost;
}
