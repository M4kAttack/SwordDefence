using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private HapticFeedback hapticFeedback;
    private SoundHandler soundHandler;
    private Transform lookDirection;
    //Timer 
    private float explosionTime = 20;
    private float explode;

    private bool exploded = false;

    private MeshRenderer meshRenderer;
    private GameObject fxExplosion;
    private Collider damageZoneCollider;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        hapticFeedback = transform.root.GetComponent<HapticFeedback>();
        soundHandler = GameObject.FindGameObjectWithTag("SoundHandler").GetComponent<SoundHandler>();
        lookDirection = GameObject.FindGameObjectWithTag("Player").transform.Find("TrackingSpace/CenterEyeAnchor");
        rigidbody = GetComponent<Rigidbody>();
        damageZoneCollider = GetComponent<SphereCollider>();
        damageZoneCollider.enabled = false;
        meshRenderer = GetComponent<MeshRenderer>();
        fxExplosion = transform.Find("FX_Explosion").gameObject;
        fxExplosion.SetActive(false);
        explode = Time.time + explosionTime;

    }

    // Update is called once per frame
    void Update()
    {

      
        if (transform.position.y < 0.05f && !exploded)
        {
            soundHandler.PlayExplosion(transform.position);
            damageZoneCollider.enabled = true;
            exploded = true;
            meshRenderer.enabled = false;
            fxExplosion.SetActive(true);
            Invoke("DisableCollider", 0.1f);
            Invoke("Disable", 1f);
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
        var gameobjectHit = collision.gameObject;
        var root = gameobjectHit.transform.root;

        if (root.CompareTag("Enemy"))
        {
            var isHeadShot = true;
            root.GetComponent<EnemyHit>().KillEnemy(isHeadShot);
        }

        if(gameobjectHit.CompareTag("LightSabre"))
        {
            hapticFeedback.Vibrate(1f, 1f, 1f, gameobjectHit.GetComponent<LightSabre>().controller);
            transform.rotation = gameobjectHit.transform.rotation;
            rigidbody.AddForce(lookDirection.forward * 20, ForceMode.VelocityChange);
        }
        
    }
}
