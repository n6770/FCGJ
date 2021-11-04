using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnInterval = 1f;
    public float timeElapsed = 0f;
    public float spawnMultiplier = 0.2f;
    public float increaseMultiplier = 0.01f;
    public GameObject[] spawnpoints;
    public GameObject[] enemies;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > spawnInterval)
        {
            timeElapsed = 0f;
            float rand = Random.Range(0f, 1f);

            if (rand < spawnMultiplier)
            {
                SpawnEnemy();
                spawnMultiplier += increaseMultiplier;
            }

            
        }
    }

    void SpawnEnemy()
    {
        int enemySpawn = Random.Range(0, enemies.Length + 1);
        int spawnPoint = Random.Range(0, spawnpoints.Length + 1);

        GameObject spawnPointGo = spawnpoints[spawnPoint];
        

        if (Vector3.Distance(spawnPointGo.transform.position, player.transform.position) < 5f)
        {
            SpawnEnemy();
            return;
        }
        else
        {
            Instantiate(enemies[enemySpawn], spawnPointGo.transform.position, Quaternion.identity);
        }
    }
}
