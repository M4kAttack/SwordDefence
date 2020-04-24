using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyManager : MonoBehaviour
{
   
    public GameObject jumpingEnemy;
    public GameObject grenadeEnemy;
    private List<GameObject> enemies = new List<GameObject>();
    private int enemyListIndex = 0;
    private GameObject[] spawnPositions;
    private int percentageOfGrenadeThrowers = 10;

    private int enemiesKilled = 0;
    public int activeEnemies = 0;
    private int maxEnemies = 7;
    public int level { get; set; } = 1;


    //SpawnTimer
    private float spawnTime = 3f;
    private float nextSpawn;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 100; i++)
        {
            //var newJumpingEnemy = Instantiate(jumpingEnemy);
            //newJumpingEnemy.SetActive(false);
            //enemies.Add(newJumpingEnemy);
            //if(i % percentageOfGrenadeThrowers == 0)
            //{
                var newGrenadeEnemy = Instantiate(grenadeEnemy);
                newGrenadeEnemy.SetActive(false);
                enemies.Add(newGrenadeEnemy);
            //}
 
        }
        spawnPositions = GameObject.FindGameObjectsWithTag("SpawnPosition");

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
           activeEnemies++;
            var randomSpawn = UnityEngine.Random.Range(0, spawnPositions.Length);
            enemies[enemyListIndex].transform.position = spawnPositions[randomSpawn].transform.position;
            enemies[enemyListIndex].SetActive(true);
            enemyListIndex++;
    }

    internal void EnemyKilled()
    {
        enemiesKilled++;
        activeEnemies--;
    }
}
