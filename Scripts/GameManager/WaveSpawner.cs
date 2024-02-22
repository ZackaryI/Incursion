using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] float spawnDelay = .4f;
    private float spawnDelayTimer;
    private NodePathing nodePathing;
    private ShopManager shopManager;

    [SerializeField] float StartingValue;
    [SerializeField] float DifficultyIncreaseSlope;
    [SerializeField] randomWaveSpawnerWaves[] waveLevels;
    [SerializeField] randomWaveSpawnerWaves[] bossWaves;
    [SerializeField] int bossWaveFrequency = 5;
    [SerializeField] bool isTutorial = false;

    private waveTimer timer;
    private int currentWave = 0;
    private int currentWaveLevel = 0;
    private int waveLevelTracker = 1;
    private float money = 0;
    private bool startNextWave = true;
    private bool maxWaveReached = false;
    readonly List<GameObject> units = new();

    bool spawnBlocked = false;
    bool gameStart = false;
    private void OnTriggerEnter(Collider other)
    {
        spawnBlocked = true;
    }
    private void OnTriggerExit(Collider collision)
    {
        spawnBlocked = false;
    }
    private void Start()
    {
        spawnDelayTimer = spawnDelay;
        shopManager = GetComponent<ShopManager>();
        nodePathing = GetComponent<NodePathing>();
        timer = GetComponent<waveTimer>();

        if (!isTutorial)
        {
            Time.timeScale = 1.0f;
        }
        else 
        {
            Time.timeScale = 0;
        }
    }
    private void FixedUpdate()
    {
        if (startNextWave && !spawnBlocked && gameStart == true)
        {
            if (spawnDelayTimer <= 0 && money > 0)
            {
                if (waveLevelTracker % bossWaveFrequency == 0) 
                {
                    SpawnBoss();
                }
                Spawn();
                spawnDelayTimer = spawnDelay;
            }
            else if (spawnDelayTimer > 0 && money > 0)
            {
                spawnDelayTimer -= Time.deltaTime;
            }
            else if (money <= 0)
            {
                currentWave++;
                waveLevelTracker++;
                timer.ContinueWaveTimer(true);
                startNextWave = false;
            }
        }
        if (!startNextWave)
        {
            if (units.Count != 0)
            {
                if (units[0] == null)
                {
                    units.RemoveAt(0);
                }
            }
            else
            {
                startNextWave = true;
            }

        }
    }
    public void StartSpawningNextWave() 
    {
        if (waveLevelTracker  > waveLevels[currentWaveLevel].waveLength  && !maxWaveReached) 
        {
            try
            {
                currentWaveLevel++;
                waveLevelTracker = 1;
            }
            catch
            {
                maxWaveReached = true;
            }
        }

        if (currentWaveLevel > waveLevels.Length) { currentWaveLevel = 0; }
        money = AllocateMoney(currentWave);
        timer.ContinueWaveTimer(false);
        gameStart = true;
    }
    float AllocateMoney(float wave) 
    {
        return (DifficultyIncreaseSlope * wave + StartingValue);
    }
   void Spawn() 
    {
        int enemyToSpawn = Random.Range(0, waveLevels[currentWaveLevel].avaliableEnemies.Length);

        money -= waveLevels[currentWaveLevel].avaliableEnemyCost[enemyToSpawn];
        GameObject enemy = Instantiate(waveLevels[currentWaveLevel].avaliableEnemies[enemyToSpawn], start.position, Quaternion.identity);
        units.Add(enemy);
        enemy.GetComponent<EnemyController>().EnemyConstructor(nodePathing, shopManager);
    }
    void SpawnBoss() 
    {
        money = 0;
        int enemyToSpawn = Random.Range(0, bossWaves.Length - 1);
        GameObject enemy = Instantiate(waveLevels[currentWaveLevel].avaliableEnemies[enemyToSpawn], start.position, Quaternion.identity);
        units.Add(enemy);
        enemy.GetComponent<EnemyController>().EnemyConstructor(nodePathing, shopManager);
    }
}
