using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    //Timer 
    private float explosionTime = 5;
    private float explode;

    private bool exploded = false;

    private MeshRenderer meshRenderer;
    private GameObject fxExplosion;
    private TextMeshPro timer;
    private float timeAtStart;
    private Collider damageZoneCollider;
    // Start is called before the first frame update
    void Start()
    {
        damageZoneCollider = GetComponent<Collider>();
        damageZoneCollider.enabled = false;
        meshRenderer = GetComponent<MeshRenderer>();
        timeAtStart = Time.time;
        timer = transform.Find("Timer").GetComponent<TextMeshPro>();
        fxExplosion = transform.Find("FX_Explosion").gameObject;
        fxExplosion.SetActive(false);
        explode = Time.time + explosionTime;
        Invoke("Disable", explosionTime + 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(!exploded)
        {
            timer.text = Math.Round((explosionTime - (Time.time - timeAtStart))).ToString();
        }
      
        if (explode < Time.time)
        {
            damageZoneCollider.enabled = true;
            exploded = true;
            meshRenderer.enabled = false;
            fxExplosion.SetActive(true);
            Invoke("DisableCollider", 1f);
            timer.enabled = false;
        }
    }
    public void DisableCollider()
    {
        damageZoneCollider.enabled = false;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var bodyPart = collision.gameObject;
        var root = bodyPart.transform.root;

        if (root.CompareTag("Enemy"))
        {
            var isHeadShot = true;
            root.GetComponent<EnemyHit>().KillEnemy(isHeadShot);
        }
        
    }
}
