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
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            NullCheck.CheckIfNull(player, typeof(GameObject), this, "Player");
        }
        if(rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
            NullCheck.CheckIfNull(rigidbody, typeof(Rigidbody), this);
        }
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
