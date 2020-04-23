using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyManager : MonoBehaviour
{
    public GameObject jumpingEnemy;
    public GameObject lowEnemy;
    private List<GameObject> enemies = new List<GameObject>();
    private int enemyListIndex = 0;
    private GameObject[] spawnPositions;
    public int ActiveEnemies { get; set; } = 0;
    public int level { get; set; } = 1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            var newJumping = Instantiate(jumpingEnemy);
            newJumping.SetActive(false);
            enemies.Add(newJumping);
            var newLow = Instantiate(lowEnemy);
            newLow.SetActive(false);
            enemies.Add(newLow);
        }
        spawnPositions = GameObject.FindGameObjectsWithTag("SpawnPosition");

    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveEnemies == 0)
        {
            level++;
            SpawnEnemies();
        } 
    }

    private void SpawnEnemies()
    {

        for (int i = 0; i < level * 2; i++)
        {
            ActiveEnemies++;
            var randomSpawn = UnityEngine.Random.Range(0, spawnPositions.Length);
            enemies[enemyListIndex].transform.position = spawnPositions[randomSpawn].transform.position;
            enemies[enemyListIndex].SetActive(true);
            enemyListIndex++;
        }
    }
}
