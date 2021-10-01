using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawmPoint;

    public float TimebetweenWaves = 5.5f;

    public Text waveCountdownTimer;

    private float countDown = 2f;

    public GameManager gameManager;

    private int WaveIndex = 0;

    private void Update()
    {
        if(EnemiesAlive > 0)
        {
            return;
        }

        if (WaveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = TimebetweenWaves;
            return;
        }

        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        waveCountdownTimer.text = string.Format("{0:00.00}", countDown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[WaveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        WaveIndex++;

    }   

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawmPoint.position, spawmPoint.rotation);

    }
}
