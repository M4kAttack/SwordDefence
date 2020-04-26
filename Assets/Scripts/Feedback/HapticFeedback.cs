using UnityEngine;

public class HapticFeedback : MonoBehaviour
{
    private bool vibrate = false;

    private float vibrateTime = 0;
    private float stopVibrate;


    // Update is called once per frame
    void Update()
    {
        StopVibration();
    }

    private void StopVibration()
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
        var valid = ControllerCheck.ValidControllerDontThrow(controller);
        if (valid)
        {
        vibrateTime = time;
        stopVibrate = Time.time + vibrateTime;
        vibrate = true;
        OVRInput.SetControllerVibration(frequency, frequency, controller);
        }
    }
}
