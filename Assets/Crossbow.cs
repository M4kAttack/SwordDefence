using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    private OVRInput.Controller controller = OVRInput.Controller.RTouch;
    public GameObject arrow;
    private GameObject animationArrow;
    private Transform forcePosition;
    private Animator animator;
    private float arrowSpeed = 40;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animationArrow = transform.Find("arrow").gameObject;
        forcePosition = transform.Find("ForcePosition");
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            animator.SetTrigger("FireArrow");
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
        animationArrow.SetActive(true);
    }
}
