using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneStyler : MonoBehaviour
{
    public Material[] mats;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount % 10 == 0)
        {
            var random = UnityEngine.Random.Range(0, mats.Length - 1);
            meshRenderer.material = mats[random];
        }
    }
}
