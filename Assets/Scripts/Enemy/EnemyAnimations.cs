using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private SoundHandler soundHandler;
    private MoveEnemy moveEnemy;
    private Animator animator;
    private EnemyType enemyType;
    private GameObject player;
    private bool grenadeThrown = false;
    private GameObject grenade;
    private Rigidbody grenadeRigidbody;
    private float grenadeThrowForce = 14;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        soundHandler = GameObject.FindGameObjectWithTag("SoundHandler").GetComponent<SoundHandler>();
        if (transform.name.Contains("Jump"))
        {
            enemyType = EnemyType.NoGrenade;
        } else if(transform.name.Contains("Grenade"))
        {
            enemyType = EnemyType.Grenade;
            grenade = FindGameObject.FindChildByTag(gameObject, "Grenade");
            grenadeRigidbody = grenade.GetComponent<Rigidbody>();
        }
        animator = GetComponent<Animator>();
        moveEnemy = GetComponent<MoveEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyType == EnemyType.Grenade && !grenadeThrown)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 20)
            {
                grenadeThrown = true;
                animator.SetTrigger("ThrowGrenade");
            };
        }
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                JumpAttack();

        }
    }

   

    private void JumpAttack()
    {
            soundHandler.PlayJumpStart(transform.position);
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

    public void ThrowGrenadeByEvent()
    {
        grenade.transform.parent = null;
        grenadeRigidbody.isKinematic = false;
        grenadeRigidbody.AddForce(-grenade.transform.forward * grenadeThrowForce, ForceMode.VelocityChange);

    }
}
