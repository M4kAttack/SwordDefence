using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private GameObject LightSabre;
    private GameObject crossbow;
    public OVRInput.Controller controller;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {


        LightSabre = transform.Find("LightSabre").gameObject;
        crossbow = transform.Find("Crossbow").gameObject;
        if(controller == OVRInput.Controller.RTouch)
        {
            LightSabre.SetActive(false);
            animator = GameObject.FindGameObjectWithTag("RightHandSkeletal").GetComponent<Animator>();
        } else
        {
            crossbow.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {

            if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
            animator.SetBool("PullingTrigger", true);
            }
           if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
           {
            animator.SetBool("PullingTrigger", false);
           }


    }

}
