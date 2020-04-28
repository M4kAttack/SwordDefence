using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxStyler : MonoBehaviour
{

    public Material[] skybox;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 20 == 0)
        {
            ChangeMaterial();
        }
    }

    private void ChangeMaterial()
    {
    
        RenderSettings.skybox = skybox[index++];
        if (index == skybox.Length - 1)
            index = 0;
    }
}
