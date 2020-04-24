using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
   private ScoreGameManager scoreGameManager;
   public int enemyDeathScore = 10;
   private TextMeshPro scoreText;
   private GameEnemyManager gameEnemyManager;
   private Rigidbody[] rigidbodies;
   private Collider[] colliders;

   public GameObject head;
    private GameObject fXBloodSplatter;

    private bool dead = false;

   private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform tr in transform.GetComponentsInChildren<Transform>())
        {
            if (tr.tag == "Head")
            {
                head = tr.gameObject;
            }
        }

        fXBloodSplatter = transform.Find("FX_BloodSplatter").gameObject;
        fXBloodSplatter.SetActive(false);
         scoreText = transform.Find("ScoreText").GetComponent<TextMeshPro>();
        scoreText.text = enemyDeathScore.ToString();
        scoreText.enabled = false;
        var managers = GameObject.FindGameObjectWithTag("GameManagers");
        gameEnemyManager = managers.GetComponent<GameEnemyManager>();
        scoreGameManager = managers.GetComponent<ScoreGameManager>();
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



    public void KillEnemy(bool isHeadShoot)
    {
        if(!dead)
        {
            dead = true;
            ModifyScoreText(isHeadShoot);
            if(isHeadShoot)
            {
                head.transform.localScale = Vector3.zero;
                fXBloodSplatter.SetActive(true);
            }
   
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
    }

    private void ModifyScoreText(bool isHeadShoot)
    {
        scoreGameManager.UpdateScore(enemyDeathScore, isHeadShoot);


        if (scoreGameManager.headShootCounter > 0)
        {
            enemyDeathScore = enemyDeathScore * scoreGameManager.headShootCounter;
            scoreText.text = enemyDeathScore.ToString();
        }

        scoreText.enabled = true;
    }

    private void DisableEnemy()
    {
        gameEnemyManager.EnemyKilled();
        gameObject.SetActive(false);
    }
}
