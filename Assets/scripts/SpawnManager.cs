using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] enemyPrefabs;
	public int enemyCount;
	private float spawnRange = 9;
	public int waveNumber = 1;
	public GameObject[] powerupPrefabs;
	public GameObject bossPrefab;
	public GameObject[] miniEnemyPrefabs;
	public int bossRound=5;

	// Start is called before the first frame update
	void Start()
	{
		int randomPowerup = Random.Range(0, powerupPrefabs.Length);
		Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(),
		            powerupPrefabs[randomPowerup].transform.rotation);
		SpawnEnemyWave(waveNumber);
	}

	private Vector3 GenerateSpawnPosition()
	{
		float spawnPosX = Random.Range(-spawnRange, spawnRange);
		float spawnPosZ = Random.Range(-spawnRange, spawnRange);
		Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
		return randomPos;
	}
	
	void SpawnBossWave(int currentRound)
{
    int miniEnemiesToSpawn;
    if (bossRound != 0)
    {
        miniEnemiesToSpawn = currentRound / bossRound;
    }
    else
    {
        miniEnemiesToSpawn = 1;
    }

    var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
    boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemiesToSpawn;
}
	public void SpawnMiniEnemy(int amount)
	{
		for(int i = 0; i < amount; i++)
		{
			int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
			Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(),
			            miniEnemyPrefabs[randomMini].transform.rotation);
		}
	}

	// Update is called once per frame
	void Update()
{
    enemyCount = FindObjectsOfType<Enemy>().Length;
    if (enemyCount == 0)
    {
        waveNumber++;
        if (bossRound != 0 && waveNumber % bossRound == 0)
        {
            SpawnBossWave(waveNumber);
        }
        else
        {
            SpawnEnemyWave(waveNumber);
        }

        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(),
                    powerupPrefabs[randomPowerup].transform.rotation);
    }
}

	void SpawnEnemyWave(int enemiesToSpawn)
	{
		for (int i = 0; i < enemiesToSpawn; i++)
		{
			int randomEnemy = Random.Range(0, enemyPrefabs.Length);
			Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation);
		}
	}
}
