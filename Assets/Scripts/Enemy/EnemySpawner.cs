using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameEnemyManager gameEnemyManager;
    // Start is called before the first frame update
    void Start()
    {
        gameEnemyManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameEnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
