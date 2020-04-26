using UnityEngine;

public class HandController : MonoBehaviour
{
    private GameObject lightSabre;
    private GameObject crossbow;
    public OVRInput.Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        if(lightSabre == null)
        {
            lightSabre = transform.Find("LightSabre").gameObject;
        }
        if(crossbow == null)
        {
            crossbow = transform.Find("Crossbow").gameObject;
        }
        ControllerCheck.ValidControllerThrow(controller, this);
        if (controller == OVRInput.Controller.RTouch)
        {
            lightSabre.SetActive(false);
        } else
        {
            crossbow.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft, controller) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight, controller))
        {
            if (lightSabre.activeSelf)
            {
                lightSabre.SetActive(false);
                crossbow.SetActive(true);
            }
            else
            {
                lightSabre.SetActive(true);
                crossbow.SetActive(false);
            }
        }
    }
}
