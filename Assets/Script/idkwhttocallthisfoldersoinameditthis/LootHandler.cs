using UnityEngine;

public class LootHandler : MonoBehaviour
{


    public GameObject healObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void DropLoot(string loot)
    {
        if(loot == "Heal")
        {
            Instantiate(healObj, transform.position, Quaternion.identity);
        }
    }
}
