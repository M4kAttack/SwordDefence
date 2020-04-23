using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    private AudioSource jumpSource;
    public AudioClip jumpStart;
    // Start is called before the first frame update
    void Start()
    {
        jumpSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayJumpStart()
    {
        jumpSource.PlayOneShot(jumpStart);
    }
}
