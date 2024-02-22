using UnityEngine;

public class waveTimer : MonoBehaviour
{
    [SerializeField] float timeBetweenWaves = 7f;
    private float timer = 0;
    private WaveSpawner waveSpawner;
    private TextManager text;
    private bool timerNotPuased;
    private void Start()
    {
        timerNotPuased = true;
        text = GetComponent<TextManager>();
        waveSpawner = GetComponent<WaveSpawner>();
        timer = timeBetweenWaves;
    }
    private void FixedUpdate()
    {
        if (timerNotPuased) 
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
               // text.UpdateWaveText(Mathf.RoundToInt(timer));
            }
            else
            {
                waveSpawner.StartSpawningNextWave();
                timer = timeBetweenWaves;
            }
        }
    }
    public void ContinueWaveTimer(bool continueTimer) 
    {
        timerNotPuased = continueTimer;
    }
}
