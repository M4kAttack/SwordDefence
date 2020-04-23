using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private MoveEnemy moveEnemy;
    private Animator animator;
    private EnemyType enemyType;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.name.Contains("Jump"))
        {
            enemyType = EnemyType.Jumping;
        } else if(transform.name.Contains("Low"))
        {
            enemyType = EnemyType.Low;
        }
        animator = GetComponent<Animator>();
        moveEnemy = GetComponent<MoveEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enemyType == EnemyType.Jumping)
            {
                JumpAttack();
            }

            if (enemyType == EnemyType.Low)
            {
                LowAttack();
            }
        }
    }

    private void LowAttack()
    {
        moveEnemy.speed = 1.5f;
        animator.SetTrigger("DoubleLeg");
    }

    private void JumpAttack()
    {
            var rand = UnityEngine.Random.Range(1, 3);
            if (rand == 1)
            {
                moveEnemy.speed = 0;
                animator.SetTrigger("FlipKickAttack");
            }
            else if (rand == 2)
            {
                moveEnemy.speed = 1.5f;
                animator.SetTrigger("JumpAttack");
            }
    }
}
