using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemyGameManager gameEnemyManager;
    // Start is called before the first frame update
    void Start()
    {
        gameEnemyManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
