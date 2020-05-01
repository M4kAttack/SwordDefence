using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private SoundHandler soundHandler;
    private AudioSource grenadeSource;
    private MoveEnemy moveEnemy;
    private Animator animator;
    private EnemyType enemyType;
    private GameObject player;
    private bool grenadeThrown = false;
    private GameObject grenade;
    private Rigidbody grenadeRigidbody;
    private float grenadeThrowForce = 14;
    // Start is called before the first frame update
    void Start()
    {
       
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            NullCheck.CheckIfNull(player, typeof(GameObject), this, "Player");
        }
        if(soundHandler == null)
        {
            soundHandler = GameObject.FindGameObjectWithTag("SoundHandler").GetComponent<SoundHandler>();
            NullCheck.CheckIfNull(soundHandler, typeof(SoundHandler), this);
        }

        grenade = FindGameObject.FindChildByTag(gameObject, "Grenade");
        if (grenade == null)
        {
            enemyType = EnemyType.NoGrenade;
        } else
        {
            enemyType = EnemyType.Grenade;
            grenadeRigidbody = grenade.GetComponent<Rigidbody>();
            NullCheck.CheckIfNull(grenadeRigidbody, typeof(Rigidbody), this);

            if (grenadeSource == null)
            {
                grenadeSource = grenade.GetComponent<AudioSource>();
                NullCheck.CheckIfNull(grenadeSource, typeof(AudioSource), this);
            }

        }
        if(animator == null)
        {
            animator = GetComponent<Animator>();
            NullCheck.CheckIfNull(animator, typeof(Animator), this);
        }
        if(moveEnemy == null)
        {
            moveEnemy = GetComponent<MoveEnemy>();
            NullCheck.CheckIfNull(moveEnemy, typeof(MoveEnemy), this);
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyType == EnemyType.Grenade && !grenadeThrown)
        {
            ThrowGrenade();
        }
    }

    private void ThrowGrenade()
    {
            if (Vector3.Distance(transform.position, player.transform.position) < 20)
            {
                grenadeThrown = true;
                animator.SetTrigger("ThrowGrenade");
            };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                JumpAttack();
        }
    }

   

    private void JumpAttack()
    {
            soundHandler.PlayJumpStart(transform.position);
            var rand = UnityEngine.Random.Range(1, 3);
            if (rand == 1)
            {
                moveEnemy.speed = 0;
                animator.SetTrigger("FlipKickAttack");
            }
            else if (rand == 2)
            {
                moveEnemy.speed = 1.5f;
                animator.SetTrigger("JumpAttack");
            }
    }

    public void ThrowGrenadeByEvent()
    {
        grenade.transform.parent = null;
        grenadeRigidbody.isKinematic = false;
        soundHandler.PlayGrenadeWhistle(grenadeSource);
        grenadeRigidbody.AddForce(-grenade.transform.forward * grenadeThrowForce, ForceMode.VelocityChange);
    }
}
