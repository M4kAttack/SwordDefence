using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public bool test;
    Rigidbody[] rigidbodies;
    Collider[] colliders;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
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

       gameObject.SetActive(false);
    }
}
