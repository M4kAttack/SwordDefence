using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            root.GetComponent<EnemyHit>().KillEnemy();
        } else if(collision.gameObject.CompareTag("PlayerSword"))
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }
    }
}
