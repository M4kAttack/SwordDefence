using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    private AudioSource jumpSource;

    private AudioSource crossbowSourceLeft;
    private AudioSource crossbowSourceRight;
    private AudioSource explosionSource;
    private AudioSource lightSabreSource;

    public AudioClip explosionSound;
    public AudioClip jumpStartSound;
    public AudioClip crossbowSound;
    // Start is called before the first frame update
    void Start()
    {
        explosionSource = gameObject.AddComponent<AudioSource>();
        jumpSource = gameObject.AddComponent<AudioSource>();
        var crossbows = GameObject.FindGameObjectsWithTag("Crossbow");
       var tempCrossBow = crossbows[0].GetComponent<Crossbow>();
        if(tempCrossBow.controller == OVRInput.Controller.RTouch)
        {
            crossbowSourceRight = crossbows[0].GetComponent<AudioSource>();
            crossbowSourceLeft = crossbows[1].GetComponent<AudioSource>();
        } else
        {
            crossbowSourceRight = crossbows[1].GetComponent<AudioSource>();
            crossbowSourceLeft = crossbows[0].GetComponent<AudioSource>();
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
}
