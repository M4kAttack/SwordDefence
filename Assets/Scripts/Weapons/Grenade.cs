using UnityEngine;

public class Grenade : MonoBehaviour
{
    private HapticFeedback hapticFeedback;
    private SoundHandler soundHandler;
    private AudioSource whistleSource;
    //Timer 
    private float explosionTime = 20;
    private float explode;

    private bool exploded = false;

    private MeshRenderer meshRenderer;
    private GameObject fxExplosion;
    private GameObject fxSmoke;
    private Collider blastRadius;
    private Collider boxCollider;
    private Rigidbody rigidbody;
    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        if(whistleSource == null)
        {
            whistleSource = gameObject.AddComponent<AudioSource>();
            NullCheck.CheckIfNull(whistleSource, typeof(AudioSource), this, "whistleSource");
        }
        whistleSource.spatialBlend = 1;
        whistleSource.volume = 0.3f;
        if (player == null)
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
        if(boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider>();
            NullCheck.CheckIfNull(boxCollider, typeof(BoxCollider), this);
        }
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
       if(fxSmoke == null)
        {
            var temp = transform.Find("FX_Smoke");
            NullCheck.CheckIfNull(temp, typeof(Transform), this, "FX_Smoke");
            fxSmoke = temp.gameObject;
        }
        fxSmoke.SetActive(false);
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
            fxSmoke.SetActive(true);
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
            soundHandler.StopGrenadeWhistle(whistleSource);
            var lightSabre = gameobjectHit.GetComponent<LightSabre>();
            if(lightSabre != null)
            {
                hapticFeedback.Vibrate(1f, 1f, 1f, lightSabre.controller);
                transform.parent = gameobjectHit.transform;
                meshRenderer.enabled = false;
                boxCollider.enabled = false;
                rigidbody.isKinematic = true;
                fxSmoke.SetActive(true);
                Invoke("Disable", 0.3f);
            }
        }
    }
}
