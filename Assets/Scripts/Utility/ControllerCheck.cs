using UnityEngine;

public static class ControllerCheck
{
    public static void ValidControllerThrow(OVRInput.Controller controller, Component fromClass)
    {
        if (controller != OVRInput.Controller.RTouch && controller != OVRInput.Controller.LTouch)
        {
            throw new MissingComponentException($"Controller not valid! in {fromClass} select RTouch or LTouch instead of {controller.ToString()}");
        }
    }
    public static bool ValidControllerDontThrow(OVRInput.Controller controller)
    {
        if (controller != OVRInput.Controller.RTouch && controller != OVRInput.Controller.LTouch)
        {
            return false;
        }
        return true;
    }
}
