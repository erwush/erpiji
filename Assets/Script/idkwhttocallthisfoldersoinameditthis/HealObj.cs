using UnityEngine;

public class HealObj : MonoBehaviour
{

    public float healAmount;
    public float healPercentage;

    void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "Player")
        {
            PlayerAttribute playerAttr = collision.gameObject.GetComponent<PlayerAttribute>();
            healAmount = playerAttr.maxHealth * (healPercentage/100);
            collision.gameObject.GetComponent<PlayerHealth>().HealthChange(healAmount);
            Destroy(gameObject);
        }
    } 







    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
