using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private GameEnemyManager gameEnemyManager;
   private Rigidbody[] rigidbodies;
   private Collider[] colliders;

   private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        gameEnemyManager = GameObject.FindGameObjectWithTag("GameManagers").GetComponent<GameEnemyManager>();
        animator = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            //Skip parent rb
            if(rb.useGravity == true)
            {
                rb.isKinematic = true;
                rb.mass = 100;
            }
        }

  

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void KillEnemy()
    {
        animator.enabled = false;
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        Invoke("DisableEnemy", 2f);
       
    }

    private void DisableEnemy()
    {
        gameEnemyManager.EnemyKilled();
        gameObject.SetActive(false);
    }
}
