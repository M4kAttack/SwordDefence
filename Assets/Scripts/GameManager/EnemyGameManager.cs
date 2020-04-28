using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGameManager : MonoBehaviour
{
   
    public GameObject jumpingEnemy;
    public GameObject grenadeEnemy;
    private List<GameObject> enemies = new List<GameObject>();
    private int enemyListIndex = 0;
    private GameObject[] spawnPositions;
    private int percentageOfGrenadeThrowers = 5;

    private int enemiesKilled = 0;
    public int activeEnemies = 0;
    private int maxEnemies = 7;
    public int level { get; set; } = 1;


    //SpawnTimer
    private float spawnTime = 2f;
    private float nextSpawn;
    // Start is called before the first frame update
    void Start()
    {
        NullCheck.CheckIfNull(jumpingEnemy, typeof(GameObject), this, "jumpingEnemy");
        NullCheck.CheckIfNull(grenadeEnemy, typeof(GameObject), this, "grenadeEnemy");
        InitializeEnemies(100, percentageOfGrenadeThrowers);
        if (spawnPositions == null)
        {
            spawnPositions = GameObject.FindGameObjectsWithTag("SpawnPosition");

            if(spawnPositions.Length == 0)
            {
                throw new MissingComponentException($"Spawnpositions are missing in EnemyGameManager");
            }
        }

    }

    public void InitializeEnemies(int amount, int percentageOfGrenadeThrowers)
    {
        for (int i = 0; i < amount; i++)
        {
            var newJumpingEnemy = Instantiate(jumpingEnemy);
            newJumpingEnemy.SetActive(false);
            enemies.Add(newJumpingEnemy);
            if (i % percentageOfGrenadeThrowers == 0)
            {
                var newGrenadeEnemy = Instantiate(grenadeEnemy);
                newGrenadeEnemy.SetActive(false);
                enemies.Add(newGrenadeEnemy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
            if(nextSpawn < Time.time && activeEnemies < maxEnemies)
            {
                nextSpawn = Time.time + spawnTime;
                SpawnEnemy();
            }
    }
   
    private void SpawnEnemy()
    {
        if(enemyListIndex < enemies.Count -1)
        {
           activeEnemies++;
            var randomSpawn = UnityEngine.Random.Range(0, spawnPositions.Length);
            enemies[enemyListIndex].transform.position = spawnPositions[randomSpawn].transform.position;
            enemies[enemyListIndex].SetActive(true);
            enemyListIndex++;
        } else
        {
            NextLevel();
        }
    }

    internal void EnemyKilled()
    {
        enemiesKilled++;
        activeEnemies--;
    }

    private void NextLevel()
    {
        throw new NotImplementedException();
    }
}
