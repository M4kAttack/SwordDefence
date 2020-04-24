using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
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
        hapticFeedback = transform.root.GetComponent<HapticFeedback>();
        animator = GetComponent<Animator>();
        animationArrow = transform.Find("arrow").gameObject;
        forcePosition = transform.Find("ForcePosition");
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
