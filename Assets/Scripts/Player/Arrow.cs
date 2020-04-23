using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float timeAtInstansiation = 0;
    private float timeTilDisable = 3f;
    private void Start()
    {
        timeAtInstansiation = Time.time;
    }
    private void Update()
    {
        if(timeTilDisable + timeAtInstansiation < Time.time)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        var root = collision.gameObject.transform.root;
        if (root.CompareTag("Enemy"))
        {
            root.GetComponent<EnemyHit>().KillEnemy();
            gameObject.SetActive(false);
        }
 
    }
}
