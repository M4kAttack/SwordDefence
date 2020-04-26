using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    private MusicResponseGameManager musicResponseGameManager;


    private AudioSource jumpSource;
    private AudioSource backgroundMusicSouce;
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
    public AudioClip lightSabreMove;
    public AudioClip[] backgroundMusic;


    private int songIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(musicResponseGameManager == null)
        {
            musicResponseGameManager = GameObject.FindGameObjectWithTag("GameManagers").GetComponent<MusicResponseGameManager>();
            NullCheck.CheckIfNull(musicResponseGameManager, typeof(MusicResponseGameManager), this);
        }

        if (backgroundMusicSouce == null) 
        {
            backgroundMusicSouce = gameObject.AddComponent<AudioSource>();
            NullCheck.CheckIfNull(backgroundMusicSouce, typeof(AudioSource), this);
            backgroundMusicSouce.volume = 0.2f;
        }
        backgroundMusicSouce.PlayOneShot(backgroundMusic[songIndex++]);

        if(explosionSource == null)
        {
            explosionSource = gameObject.AddComponent<AudioSource>();
            NullCheck.CheckIfNull(explosionSource, typeof(AudioSource), this);
            explosionSource.spatialBlend = 1;
            explosionSource.volume = 0.5f;
        }

        if(jumpSource == null)
        {
            jumpSource = gameObject.AddComponent<AudioSource>();
            NullCheck.CheckIfNull(jumpSource, typeof(AudioSource), this);
            jumpSource.spatialBlend = 1;
        }

        AssignCrossBowSources();
        AssignLightSabreSources();

    }

    private void AssignCrossBowSources()
    {
        var crossbows = GameObject.FindGameObjectsWithTag("Crossbow");
        var tempCrossBow = crossbows[0].GetComponent<Crossbow>();
        NullCheck.CheckIfNull(tempCrossBow, typeof(Crossbow), this);
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
        var tempsabre = tempSabres[0].GetComponent<LightSabre>();
        NullCheck.CheckIfNull(tempsabre, typeof(LightSabre), this);
        if (tempsabre.controller == OVRInput.Controller.RTouch)
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
        if(!backgroundMusicSouce.isPlaying)
        {
            PlayNextSong();
        }
    }

    public void PlayNextSong()
    {
        backgroundMusicSouce.PlayOneShot(backgroundMusic[songIndex++]);
        if(songIndex == backgroundMusic.Length -1)
        {
            songIndex = 0;
        }
    }
    public void PlayJumpStart(Vector3 jumpPosition)
    {
        jumpSource.transform.position = jumpPosition;
        jumpSource.PlayOneShot(jumpStartSound);
    }

    public void PlayCrossBowShoot(Crossbow crossbow)
    {
        if(crossbow != null)
        {
            var sourceToPlay = crossbow.controller == OVRInput.Controller.RTouch ? crossbowSourceRight : crossbowSourceLeft;
            sourceToPlay.PlayOneShot(crossbowSound);
        }
    }

    public void PlayExplosion(Vector3 explosionPosition)
    {
        explosionSource.transform.position = explosionPosition;
        explosionSource.PlayOneShot(explosionSound);
    }

    public void PlayLaserHit(LightSabre lightSabre)
    {
        if(lightSabre != null)
        {
            var sourceToPlay = lightSabre.controller == OVRInput.Controller.RTouch ? lightSabreSourceRight : lightSabreSourceLeft;
            sourceToPlay.PlayOneShot(lightSabreHitSound);
        }
     
    }

    public void PlayEnemyHeadShot(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(enemyHeadShot);
        }
    }

    public void PlayLightsabreMove(LightSabre lightSabre)
    {
        if (lightSabre != null)
        {
            var sourceToPlay = lightSabre.controller == OVRInput.Controller.RTouch ? lightSabreSourceRight : lightSabreSourceLeft;
            sourceToPlay.PlayOneShot(lightSabreMove);
        }
    }
}
