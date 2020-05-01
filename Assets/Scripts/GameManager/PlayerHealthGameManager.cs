using TMPro;
using UnityEngine;

public class PlayerHealthGameManager : MonoBehaviour
{
    private GameObject billboard;
    private TextMeshPro healthText;
    private TextMeshPro staticHealthText;
    public Material red;
    private Material originalMaterial;
 
    public int Health { get; set; }
    public bool dead { get; set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        if (billboard == null)
        {
            billboard = GameObject.FindGameObjectWithTag("BillBoard");
            NullCheck.CheckIfNull(billboard, typeof(GameObject), this, "BillBoard");
        }

        healthText = billboard.transform.Find("HealthText").GetComponent<TextMeshPro>();
        NullCheck.CheckIfNull(healthText, typeof(TextMeshPro), this, "HealthText");
        staticHealthText = billboard.transform.Find("StaticHealthText").GetComponent<TextMeshPro>();
        NullCheck.CheckIfNull(staticHealthText, typeof(TextMeshPro), this, "StaticHealthText");
        Health = 100;
        healthText.text = Health.ToString();
        originalMaterial = healthText.material;
    }


    public void  CheckIfDead()
    {
        if(Health <= 0)
        {
            dead = true;
        }
    }
 
    public void PlayerHit(int damage)
    {
        Health -= damage;
        CheckIfDead();
        healthText.text = Health.ToString();
        if(Health > 40)
        {
            healthText.color = Color.green;
            staticHealthText.color = Color.green;
        }
        else if(Health < 41 && Health > 21)
        {
            healthText.color = Color.yellow;
            staticHealthText.color = Color.yellow;
        } else if (Health < 21)
        {
            healthText.color = Color.red;
            staticHealthText.color = Color.red;
        }
    }

    public void PlayerHealthIncrease(int increase)
    {
        Health += increase;
        healthText.text = Health.ToString();
    }

   
}
