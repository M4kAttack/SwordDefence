using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public OVRInput.Controller controller;
    private HapticFeedback hapticFeedback;
    // Start is called before the first frame update
    void Start()
    {
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
        } else if(collision.gameObject.CompareTag("PlayerSword"))
        {
            hapticFeedback.Vibrate(0.5f, 0.5f, 0.1f, controller);
        }
    }
}
