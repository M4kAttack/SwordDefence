using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxTest : MonoBehaviour
{
    public Material one;
    public Material two;
    public Material three;

    private float changeTime = 0.1f;
    private float change = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(change < Time.time)
        {
            change = Time.time + changeTime;
            if(RenderSettings.skybox == one)
            {
                RenderSettings.skybox = two;
            } else if(RenderSettings.skybox == two)
            {
                RenderSettings.skybox = three;
           } else
            {
                RenderSettings.skybox = one;
            }
         

        }

    }
}
