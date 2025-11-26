using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created    
    public DifficultySystem diff;

    public float demeg;
    public Animator anim;
    private int idx;

    public EnemyStat stat;
    public EnemyMovement enemyMovement;
    public PlayerHealth health;
    private PlayerAttribute plAttr;
    public Transform attackPoint;
    public BuffManager buffController;
    public LayerMask pLayer;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (plAttr == null)
            {
                plAttr = collision.gameObject.GetComponent<PlayerAttribute>();

            }
            // demeg = GameUtils.DamageApplier(stat.atk, plAttr.def, stat.dmgType[idx], plAttr.dmgRes[idx], stat.elemDmg[idx], plAttr.elemRes[idx], 0, 0, idx);

            // collision.gameObject.GetComponent<PlayerHealth>().HealthChange(-demeg);

        }

    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
    }


    void Update()
    {
    }

    void FinishAttacking()
    {
        if (anim.GetBool("isAttacking") == true)
        {
            anim.SetBool("isAttacking", false);
        }
    }

    public void Attack()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, stat.atkRange, pLayer);
        enemyMovement.atkTimer = stat.atkSpd;
        if (plAttr == null)
        {
            plAttr = hits[0].gameObject.GetComponent<PlayerAttribute>();

        }
        if (hits.Length > 0)
        {
            demeg = GameUtils.DamageApplier(stat.atk, plAttr.def, stat.dmgType[idx], plAttr.dmgRes[idx], stat.elemDmg[idx], plAttr.elemRes[idx], 0, 0, idx);
            hits[0].GetComponent<PlayerHealth>().HealthChange(-demeg);
            Debug.Log("demeg musuh:" + demeg);
            if (buffController.enemyBuff.Contains("Knockback"))
            {
                hits[0].GetComponent<PlayerMovement>().Knockback(transform, stat.knockback);
            }
        }
    }
}
