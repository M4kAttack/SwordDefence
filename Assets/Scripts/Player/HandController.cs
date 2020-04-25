using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private GameObject LightSabre;
    private GameObject crossbow;
    public OVRInput.Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        LightSabre = transform.Find("LightSabre").gameObject;
        crossbow = transform.Find("Crossbow").gameObject;
        if(controller == OVRInput.Controller.RTouch)
        {
            LightSabre.SetActive(false);
        } else
        {
            crossbow.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //switch hands
        if(OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft, controller) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight, controller))
        {
            if(LightSabre.activeSelf)
            {
                LightSabre.SetActive(false);
                crossbow.SetActive(true);
            } else
            {
                LightSabre.SetActive(true);
                crossbow.SetActive(false);
            }
        }
    }

}
