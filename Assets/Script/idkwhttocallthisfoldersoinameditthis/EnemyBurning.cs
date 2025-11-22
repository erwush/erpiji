using UnityEngine;
using System.Collections;

public class EnemyBurning : MonoBehaviour
{

    public float burnDmg;
    public float burnDelay;
    public float burnDuration;
    public float burnTick;
    public bool isBurning;
    public EnemyHealth enemyHealth;
    public SpriteRenderer sprite;
    public Coroutine activeCor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBurning && activeCor == null)
        {
            burnTick = burnDuration;
            sprite.color = Color.red;
        }
        if (burnTick > 0)
        {
            burnTick -= Time.deltaTime;
            if (activeCor == null)
            {
                activeCor = StartCoroutine(Burn());
            }
        }
        else
        {
            isBurning = false;
            activeCor = null;

        }
    }
    
    public IEnumerator Burn()
    {
        while (isBurning)
        {
            enemyHealth.HealthChange(-burnDmg);
            yield return new WaitForSeconds(burnDelay);
        }
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
         Destroy(this);   
    }
}
