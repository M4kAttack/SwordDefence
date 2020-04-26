using UnityEngine;

public class PlaneStyler : MonoBehaviour
{
    public Material[] mats;
    private MeshRenderer meshRenderer;
    public float changeMaterialTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        if(meshRenderer == null)
        {
           meshRenderer = GetComponent<MeshRenderer>();
           NullCheck.CheckIfNull(meshRenderer, typeof(MeshRenderer), this);
        }

        if(mats.Length == 0)
        {
            throw new MissingComponentException($"Materials missing in PlaneStyler");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % changeMaterialTime == 0)
        {
            ChangeMaterial();
        }
    }

    private void ChangeMaterial()
    {
            var random = UnityEngine.Random.Range(0, mats.Length - 1);
            meshRenderer.material = mats[random];
    }
}
