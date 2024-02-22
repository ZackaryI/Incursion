using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSpawner : MonoBehaviour
{
    [SerializeField] Wave[] wave;

    [SerializeField] Transform start;
    [SerializeField] float spawnDelay = .4f;
    [SerializeField] GameObject WinScreen;
    [SerializeField] TextMeshProUGUI scoreTextWin;
    private float spawnDelayTimer;
    private NodePathing nodePathing;
    private ShopManager shopManager;

    private int currentWave;
    private bool startNextWave;
    private int curretEnemy = 0;

    readonly List<GameObject> units = new();

    bool spawnBlocked = false;
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
        shopManager = GetComponent<ShopManager>();
        nodePathing = GetComponent<NodePathing>();
        spawnDelayTimer = spawnDelay;
        currentWave = 0;
        startNextWave = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && currentWave < wave.Length - 1)
        {
            currentWave++;
            Debug.Log("Next wave to spawn: " + currentWave + " Max Waves: " + (wave.Length - 1));
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("Max Level");
        }
    }
    private void FixedUpdate()
    {
        if (startNextWave)
        {
            if (!spawnBlocked)
            {
                if (spawnDelayTimer <= 0)
                {
                    Spawn();
                    spawnDelayTimer = spawnDelay;
                }
                else if (spawnDelayTimer > 0)
                {
                    spawnDelayTimer -= Time.deltaTime;
                }
            }
        }
        if(!startNextWave)
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

    private void Spawn()
    {
        try
        {
            if (wave[currentWave] == null) { }
        }
        catch 
        {
            scoreTextWin.text = "Score: " + shopManager.GetScore();
            scoreTextWin.gameObject.SetActive(true);
            WinScreen.SetActive(true);
            Time.timeScale = 0;
        }
        try
        {
            GameObject enemy = Instantiate(wave[currentWave].wave[curretEnemy], start.position, Quaternion.identity) as GameObject;
            enemy.GetComponent<EnemyController>().EnemyConstructor(nodePathing, shopManager);
            units.Add(enemy);
            curretEnemy++;
        }
        catch
        {
            currentWave++;
            curretEnemy = 0;
            startNextWave = false;
        }

    }
}

