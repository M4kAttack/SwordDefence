using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float speed = 3f;
    private float originalSpeed;
    private Rigidbody rigidbody;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        transform.LookAt(player.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = transform.forward * speed;
        rigidbody.velocity = new Vector3(movement.x, rigidbody.velocity.y, movement.z);
    }


    public void TurnOffSpeed()
    {
        speed = 0;
    }

    public void TurnOnSpeed()
    {
        speed = originalSpeed;
    }
}
