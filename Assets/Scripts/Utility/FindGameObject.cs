using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindGameObject : MonoBehaviour
{

    /// <summary>
    /// Find a child by tag. not for multiple children with same tag 
    /// </summary>
    /// <returns>Last occurrence or null</returns>
 public static GameObject FindChildByTag(GameObject parent, string tag)
    {
        GameObject child = null;
        foreach (Transform tr in parent.transform.GetComponentsInChildren<Transform>())
        {
            if (tr.tag == tag)
            {
                child = tr.gameObject;
            }
        }

        return child;
    }
}
