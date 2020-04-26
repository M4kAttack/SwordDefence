using UnityEngine;

public class Grenade : MonoBehaviour
{
    private HapticFeedback hapticFeedback;
    private SoundHandler soundHandler;
    private Transform lookDirection;
    //Timer 
    private float explosionTime = 20;
    private float explode;

    private bool exploded = false;

    private MeshRenderer meshRenderer;
    private GameObject fxExplosion;
    private Collider blastRadius;
    private Rigidbody rigidbody;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        { 
            player = GameObject.FindGameObjectWithTag("Player");
            NullCheck.CheckIfNull(player, typeof(GameObject), this, "GameObject");
        }
        if(hapticFeedback == null)
        {
            hapticFeedback = player.GetComponent<HapticFeedback>();
            NullCheck.CheckIfNull(hapticFeedback, typeof(HapticFeedback), this);
        }
        if(soundHandler == null)
        {
            soundHandler = GameObject.FindGameObjectWithTag("SoundHandler").GetComponent<SoundHandler>();
            NullCheck.CheckIfNull(soundHandler, typeof(SoundHandler), this);
        }
        lookDirection = player.transform.Find("TrackingSpace/CenterEyeAnchor");
        NullCheck.CheckIfNull(lookDirection, typeof(GameObject), this, "CenterEyeAnchor");
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
            NullCheck.CheckIfNull(rigidbody, typeof(Rigidbody), this);
        }
        if(blastRadius == null)
        {
            blastRadius = GetComponent<SphereCollider>();
            NullCheck.CheckIfNull(blastRadius, typeof(SphereCollider), this);
        }
        blastRadius.enabled = false;

        if(meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
            NullCheck.CheckIfNull(meshRenderer, typeof(MeshRenderer), this);
        }
       if(fxExplosion == null)
        {
           var temp = transform.Find("FX_Explosion");
            NullCheck.CheckIfNull(temp, typeof(Transform), this, "FX_Explosion");
            fxExplosion = temp.gameObject;
        }
        fxExplosion.SetActive(false);
        explode = Time.time + explosionTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.05f && !exploded)
        {
            soundHandler.PlayExplosion(transform.position);
            blastRadius.enabled = true;
            exploded = true;
            meshRenderer.enabled = false;
            fxExplosion.SetActive(true);
            Invoke("DisbaleBlastRadius", 0.1f);
            Invoke("Disable", 1f);
        }
    }
    public void DisbaleBlastRadius()
    {
        blastRadius.enabled = false;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var gameobjectHit = collision.gameObject;
        var root = gameobjectHit.transform.root;

        if (root.CompareTag("Enemy"))
        {
            var isHeadShot = true;
            var enemyHit = root.GetComponent<EnemyHit>();
            if(enemyHit != null)
            {
                enemyHit.KillEnemy(isHeadShot);
            }
          
        }

        if(gameobjectHit.CompareTag("LightSabre"))
        {
            var lightSabre = gameobjectHit.GetComponent<LightSabre>();
            if(lightSabre != null)
            {
                hapticFeedback.Vibrate(1f, 1f, 1f, lightSabre.controller);
                transform.rotation = gameobjectHit.transform.rotation;
                rigidbody.AddForce(lookDirection.forward * 20, ForceMode.VelocityChange);
            }
        }
    }
}
