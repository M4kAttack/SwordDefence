using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    private SoundHandler soundHandler;
    public OVRInput.Controller controller;
    public GameObject arrow;
    private GameObject animationArrow;
    private Transform forcePosition;
    private Animator animator;
    private float arrowSpeed = 40;
    private bool canFire = true;
    private HapticFeedback hapticFeedback;

    // Start is called before the first frame update
    void Start()
    {
        NullCheck.CheckIfNull(arrow, typeof(GameObject), this, "Arrow");
        NullCheck.CheckIfNull(arrow.GetComponent<Rigidbody>(), typeof(Rigidbody), this);
        ControllerCheck.ValidControllerThrow(controller, this);
        if (soundHandler == null)
        {
            soundHandler = GameObject.FindGameObjectWithTag("SoundHandler").GetComponent<SoundHandler>();
            NullCheck.CheckIfNull(soundHandler, typeof(SoundHandler), this);
        }
        if(hapticFeedback == null)
        {
            hapticFeedback = transform.root.GetComponent<HapticFeedback>();
            NullCheck.CheckIfNull(hapticFeedback, typeof(HapticFeedback), this);
       
        }
        if(animator == null)
        {
            animator = GetComponent<Animator>();
            NullCheck.CheckIfNull(animator, typeof(Animator), this);
        }
        if(animationArrow == null)
        {
            var temp = transform.Find("arrow");
            NullCheck.CheckIfNull(temp, typeof(GameObject), this, "arrow");
            animationArrow = temp.gameObject;
        }
         if(forcePosition == null)
        {
            forcePosition = transform.Find("ForcePosition");
            NullCheck.CheckIfNull(forcePosition, typeof(GameObject), this, "ForcePosition");
        }
     
    }
    private void OnEnable()
    {
        canFire = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(canFire && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            canFire = false;
            animator.SetTrigger("FireArrow");
            hapticFeedback.Vibrate(0.2f,0.3f, 0.5f, controller);
        }
    }

    public void FireArrowByEvent()
    {
        soundHandler.PlayCrossBowShoot(this);
        animationArrow.SetActive(false);
        var newArrow = Instantiate(arrow);
        newArrow.transform.position = animationArrow.transform.position;
        newArrow.transform.transform.rotation = animationArrow.transform.rotation;
        newArrow.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        newArrow.SetActive(true);
        var rigidbody = newArrow.GetComponent<Rigidbody>();
        rigidbody.AddForce(-newArrow.transform.forward * arrowSpeed, ForceMode.VelocityChange);
    }

    public void ActivateArrowByEvent()
    {
        canFire = true;
        animationArrow.SetActive(true);
    }
}
