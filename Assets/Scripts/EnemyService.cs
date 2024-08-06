using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveNo;
    [SerializeField] GameObject WinTheGame;
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to spawn
    public Transform[] spawnPoints; // Array of spawn points for the enemies
    public int enemiesPerWave; // Number of enemies per wave
    public float timeBetweenWaves; // Time between waves
    public float timeBetweenEnemies; // Time between spawning each enemy in a wave


    private int waveNumber = 1;
    private int maxWaves = 5;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (waveNumber <= maxWaves)
        {
            waveNo.text = "Wave: " + waveNumber.ToString();
            waveNumber++;

            if(waveNumber > maxWaves)
            {
                WinTheGame.SetActive(true);
            }

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenEnemies);
            }
            yield return new WaitForSeconds(timeBetweenWaves);


            Debug.Log("Wave " + waveNumber);            
        }

        Debug.Log("Max waves reached. Exiting coroutine.");
    }

    void SpawnEnemy()
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
