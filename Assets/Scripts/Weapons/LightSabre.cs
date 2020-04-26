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
        ControllerCheck.ValidControllerThrow(controller, this);
        if (thisLightSabre == null)
        {
            thisLightSabre = GetComponent<LightSabre>();
            NullCheck.CheckIfNull(thisLightSabre, typeof(LightSabre), this);
        }
        if(soundHandler == null)
        {
            soundHandler = GameObject.FindGameObjectWithTag("SoundHandler").GetComponent<SoundHandler>();
            NullCheck.CheckIfNull(soundHandler, typeof(SoundHandler), this);
        }
        if(hapticFeedback == null)
        {
            hapticFeedback = transform.root.GetComponent<HapticFeedback>();
            NullCheck.CheckIfNull(hapticFeedback, typeof(HapticFeedback), this);
        }
    }

    private void Update()
    {
        LightSabreVelocity();

    }

    private void LightSabreVelocity()
    {
        var velocity = OVRInput.GetLocalControllerAngularVelocity(controller).magnitude;
        //0.4f
        if (velocity > 7f)
        {
            soundHandler.PlayLightsabreMove(thisLightSabre);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var bodyPart = collision.gameObject;
        var root = bodyPart.transform.root;
        
        if (root.CompareTag("Enemy"))
        {
            var enemyHit = root.GetComponent<EnemyHit>();
            if (enemyHit != null)
            {
            hapticFeedback.Vibrate(1, 1, 0.2f, controller);
            var isHeadShot = false;
            if(bodyPart.CompareTag("Head"))
            {
                isHeadShot = true;
            }
      
                enemyHit.KillEnemy(isHeadShot);
            }
        } else if(collision.gameObject.CompareTag("LightSabre"))
        {
            hapticFeedback.Vibrate(0.5f, 0.5f, 0.1f, controller);
        }
        soundHandler.PlayLaserHit(thisLightSabre);
    }
}
