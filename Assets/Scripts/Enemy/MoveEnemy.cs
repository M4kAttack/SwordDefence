using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float speed = 30f;
    private float originalSpeed;
    private Rigidbody rigidbody;
    private GameObject player;
    private Vector3 movement;
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

        var step = transform.forward.normalized * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(transform.position + step);

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
