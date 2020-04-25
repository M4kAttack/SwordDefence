using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSabre : MonoBehaviour
{
    private SoundHandler soundHandler;
    public OVRInput.Controller controller;
    private HapticFeedback hapticFeedback;
    private LightSabre thisLightSabre;
    // Start is called before the first frame update
    void Start()
    {
        thisLightSabre = GetComponent<LightSabre>();
        soundHandler = GameObject.FindGameObjectWithTag("SoundHandler").GetComponent<SoundHandler>();
        hapticFeedback = transform.root.GetComponent<HapticFeedback>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var bodyPart = collision.gameObject;
        var root = bodyPart.transform.root;
        
        if (root.CompareTag("Enemy"))
        {
            hapticFeedback.Vibrate(1, 1, 0.2f, controller);
            var isHeadShot = false;
            if(bodyPart.CompareTag("Head"))
            {
                isHeadShot = true;
            }
            root.GetComponent<EnemyHit>().KillEnemy(isHeadShot);
        } else if(collision.gameObject.CompareTag("LightSabre"))
        {
            hapticFeedback.Vibrate(0.5f, 0.5f, 0.1f, controller);
        }
        soundHandler.PlayLaserHit(thisLightSabre);
    }
}
