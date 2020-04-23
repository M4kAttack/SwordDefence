using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticFeedback : MonoBehaviour
{
    private bool vibrate = false;

    private float vibrateTime = 0;
    private float stopVibrate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vibrate && stopVibrate < Time.time)
        {
            vibrate = false;
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
    }

    public void Vibrate(float frequency, float amplitude, float time, OVRInput.Controller controller)
    {
        vibrateTime = time;
        stopVibrate = Time.time + vibrateTime;
        vibrate = true;
        OVRInput.SetControllerVibration(frequency, frequency, controller);
    }
}
