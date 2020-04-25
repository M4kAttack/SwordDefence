using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    private AudioSource jumpSource;

    private AudioSource crossbowSourceLeft;
    private AudioSource crossbowSourceRight;
    private AudioSource explosionSource;
    private AudioSource lightSabreSourceLeft;
    private AudioSource lightSabreSourceRight;

    public AudioClip explosionSound;
    public AudioClip jumpStartSound;
    public AudioClip crossbowSound;
    public AudioClip lightSabreHitSound;
    public AudioClip enemyHeadShot;
    // Start is called before the first frame update
    void Start()
    {
        explosionSource = gameObject.AddComponent<AudioSource>();
        jumpSource = gameObject.AddComponent<AudioSource>();
        AssignCrossBowSources();
        AssignLightSabreSources();

    }


    private void AssignCrossBowSources()
    {
        var crossbows = GameObject.FindGameObjectsWithTag("Crossbow");
        var tempCrossBow = crossbows[0].GetComponent<Crossbow>();
        if (tempCrossBow.controller == OVRInput.Controller.RTouch)
        {
            crossbowSourceRight = crossbows[0].GetComponent<AudioSource>();
            crossbowSourceLeft = crossbows[1].GetComponent<AudioSource>();
        }
        else
        {
            crossbowSourceRight = crossbows[1].GetComponent<AudioSource>();
            crossbowSourceLeft = crossbows[0].GetComponent<AudioSource>();
        }
    }

    private void AssignLightSabreSources()
    {
        var tempSabres = GameObject.FindGameObjectsWithTag("LightSabre");

        if (tempSabres[0].GetComponent<LightSabre>().controller == OVRInput.Controller.RTouch)
        {
            lightSabreSourceRight = tempSabres[0].GetComponent<AudioSource>();
            lightSabreSourceLeft = tempSabres[1].GetComponent<AudioSource>();
        }
        else
        {
            lightSabreSourceRight = tempSabres[1].GetComponent<AudioSource>();
            lightSabreSourceLeft = tempSabres[0].GetComponent<AudioSource>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayJumpStart(Vector3 jumpPosition)
    {
        jumpSource.transform.position = jumpPosition;
        jumpSource.PlayOneShot(jumpStartSound);
    }

    public void PlayCrossBowShoot(Crossbow crossbow)
    {
      var sourceToPlay = crossbow.controller == OVRInput.Controller.RTouch ? crossbowSourceRight : crossbowSourceLeft;
       sourceToPlay.PlayOneShot(crossbowSound);
    }

    public void PlayExplosion(Vector3 explosionPosition)
    {
        explosionSource.transform.position = explosionPosition;
        explosionSource.PlayOneShot(explosionSound);
    }

    public void PlayLaserHit(LightSabre lightSabre)
    {
        var sourceToPlay = lightSabre.controller == OVRInput.Controller.RTouch ? lightSabreSourceRight : lightSabreSourceLeft;
        sourceToPlay.PlayOneShot(lightSabreHitSound);
    }

    public void PlayEnemyHeadShot(AudioSource audioSource)
    {
        audioSource.PlayOneShot(enemyHeadShot);
    }
}
