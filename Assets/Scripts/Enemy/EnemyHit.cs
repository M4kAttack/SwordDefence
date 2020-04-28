using TMPro;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
   private SoundHandler soundHandler;
   private ScoreGameManager scoreGameManager;
   public int enemyDeathScore = 10;
   private TextMeshPro scoreText;
   private EnemyGameManager gameEnemyManager;
   private Rigidbody[] rigidbodies;
   private Collider[] colliders;
    private AudioSource audioSource;

   public GameObject head;
    private GameObject fXBloodSplatter;

    private bool dead = false;

   private Animator animator;
    // Start is called before the first frame update
    void Start()
    {

        //TODO:Remove, this is Temporary until player damage is implemented
        Invoke("DisableEnemy", 20f);
        //TODO:Remove

        if (soundHandler == null)
        {
            soundHandler = GameObject.FindGameObjectWithTag("SoundHandler").GetComponent<SoundHandler>();
            NullCheck.CheckIfNull(soundHandler, typeof(SoundHandler), this);
        }
        if(audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            NullCheck.CheckIfNull(audioSource, typeof(AudioSource), this);
        }
        audioSource.spatialBlend = 1;
        audioSource.volume = 1f;
        if (head == null)
        {
            head = FindGameObject.FindChildByTag(gameObject, "Head");
            NullCheck.CheckIfNull(head, typeof(GameObject), this, "Head");
        }
       if(fXBloodSplatter == null)
        {
            var temp = transform.Find("FX_BloodSplatter");
            NullCheck.CheckIfNull(temp, typeof(Transform), this, "FX_BloodSplatter");
            fXBloodSplatter = temp.gameObject;
            fXBloodSplatter.SetActive(false);
        }
       if(scoreText == null)
        {
            scoreText = transform.Find("ScoreText").GetComponent<TextMeshPro>();
            NullCheck.CheckIfNull(scoreText, typeof(TextMeshPro), this);
        }
        scoreText.text = enemyDeathScore.ToString();
        scoreText.enabled = false;

        var managers = GameObject.FindGameObjectWithTag("GameManagers");
        if(gameEnemyManager == null)
        {
            gameEnemyManager = managers.GetComponent<EnemyGameManager>();
            NullCheck.CheckIfNull(gameEnemyManager, typeof(EnemyGameManager), this);
        }
        if(scoreGameManager == null)
        {
            scoreGameManager = managers.GetComponent<ScoreGameManager>();
            NullCheck.CheckIfNull(scoreGameManager, typeof(ScoreGameManager), this);
        }
        if(animator == null)
        {
            animator = GetComponent<Animator>();
            NullCheck.CheckIfNull(animator, typeof(Animator), this);
        }
        
        colliders = GetComponentsInChildren<Collider>();
        if(colliders.Length == 0)
        {
            throw new MissingComponentException($"No colliders found on enemy");
        }

        rigidbodies = GetComponentsInChildren<Rigidbody>();
        if (rigidbodies.Length == 0)
        {
            throw new MissingComponentException($"No rigidbodies found on enemy");
        }
        foreach (var rb in rigidbodies)
        {
            //Skip parent rb
            if(rb.useGravity == true)
            {
                rb.isKinematic = true;
                rb.mass = 100;
            }
        }
    }



    public void KillEnemy(bool isHeadShoot)
    {
        if(!dead)
        {
            dead = true;
            ModifyScoreText(isHeadShoot);
            if(isHeadShoot)
            {
                soundHandler.PlayEnemyHeadShot(audioSource);
                head.transform.localScale = Vector3.zero;
                fXBloodSplatter.SetActive(true);
            }
   
            animator.enabled = false;
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = false;
            }
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
            Invoke("DisableEnemy", 2f);

        }
    }

    private void ModifyScoreText(bool isHeadShoot)
    {
        scoreGameManager.UpdateScore(enemyDeathScore, isHeadShoot);

        if (scoreGameManager.headShootCounter > 0)
        {
            enemyDeathScore = enemyDeathScore * scoreGameManager.headShootCounter;
            scoreText.text = enemyDeathScore.ToString();
        }
        scoreText.enabled = true;
    }

    private void DisableEnemy()
    {
        gameEnemyManager.EnemyKilled();
        gameObject.SetActive(false);
    }
}
