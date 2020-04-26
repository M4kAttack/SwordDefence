using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicResponseGameManager : MonoBehaviour
{
    private EnemyGameManager enemyGameManager;
    // Start is called before the first frame update
    void Start()
    {
        if(enemyGameManager == null)
        {
            enemyGameManager = GameObject.FindGameObjectWithTag("GameManagers").GetComponent<EnemyGameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
