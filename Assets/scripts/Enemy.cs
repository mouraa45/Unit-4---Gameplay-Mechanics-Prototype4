using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Rigidbody enemyRB;
	private GameObject player;
	public float speed;
	public bool isBoss = false;
	public float spawnInterval;
	private float nextSpawn;
	public int miniEnemySpawnCount;
	private SpawnManager spawnManager;

	// Start is called before the first frame update
	void Start()
	{
		enemyRB = GetComponent<Rigidbody>();
		player = GameObject.Find("Player");
		if (isBoss)
		{
			spawnManager = FindObjectOfType<SpawnManager>();
		}
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 lookDirection = (player.transform.position - transform.position).normalized;
		enemyRB.AddForce(lookDirection * speed);

		if (isBoss)
		{
			if (Time.time > nextSpawn)
			{
				nextSpawn = Time.time + spawnInterval;
				spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
			}
		}

		if (transform.position.y < -10)
		{
			Destroy(gameObject);
		}
	}
}