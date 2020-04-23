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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var root = collision.gameObject.transform.root;
        if (root.CompareTag("Enemy"))
        {
            hapticFeedback.Vibrate(1, 1, 0.2f, controller);
            root.GetComponent<EnemyHit>().KillEnemy();
        } else if(collision.gameObject.CompareTag("PlayerSword"))
        {
            hapticFeedback.Vibrate(0.5f, 0.5f, 0.1f, controller);
        }
    }
}
