using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetector : MonoBehaviour
{
    private PlayerHealthGameManager playerHealthGameManager;
    private GameObject gameManagers;

    private void Start()
    {
        if (gameManagers == null)
        {
            gameManagers = GameObject.FindGameObjectWithTag("GameManagers");
            playerHealthGameManager = gameManagers.GetComponent<PlayerHealthGameManager>();
            NullCheck.CheckIfNull(playerHealthGameManager, typeof(PlayerHealthGameManager), this);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        var hitGameObject = collision.gameObject;
        var root = hitGameObject.transform.root;

        if (root.CompareTag("Enemy"))
        {
            var enemyHit = root.GetComponent<EnemyHit>();
            if (enemyHit != null)
            {
                enemyHit.DisableEnemy();
                playerHealthGameManager.PlayerHit(10);
            }

        }
        else if (collision.transform.root.CompareTag("Grenade"))
        {
            playerHealthGameManager.PlayerHit(10);
        }
    }
}
        

