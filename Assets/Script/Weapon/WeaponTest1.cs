//*LIFESTEAL TEST
using System.Collections;
using UnityEngine;

public class WeaponTest1 : WeaponBase
{


    public Animator anim;
    public float atkanim;
    public float atktimer;
    public PlayerCombat combat;
    public PlayerAttribute attr;
    public DamageBehaviour dmgBehaviour;
    public PlayerHealth healthScr;

    public float cooldown;
    public int typeIdx;
    public int elemIdx;
    public Transform atkPoint;
    public LayerMask eLayer;


    //UNIQUE VARIABLE
    public bool isHit;
    public bool hasHit;
    public float cdmInc;
    public float atkAmount;
    public Coroutine activeCor;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        combat = GetComponent<PlayerCombat>();
        attr = GetComponent<PlayerAttribute>();
        dmgBehaviour = GetComponent<DamageBehaviour>();
        anim.SetBool("isAttack1", false);
        anim.SetBool("isAttack2", false);
        atkanim = 0;
        cooldown = attr.atkSpd;
        healthScr = GetComponent<PlayerHealth>();
    }

    public override void Attack()
    {
        // if (Input.GetButton("Attack"))
        // {
        //     float chargeTime = Time.deltaTime;
        //     if(chargeTime <= 0.8f)
        //     {
        //         isCharging = true;
        //     }
        //     else
        //     {
        //         isCharging = false;
        //     }
        // }

        if (atktimer <= 0)
        {
            // StartCoroutine(AttackCoroutine());

            //if (atkanim == 2)
            //{
            // anim.SetBool("isAttack1", true);
            atkanim = Random.Range(1, 3);


            //}
            //else if (atkanim == 2)
            //{
            //    anim.SetBool("isAttack2", true);
            //    atkanim = Random.Range(1, 2);
            //}

            atktimer = cooldown;

        }
    }

    public void Update()
    {

        if (atktimer > 0)
        {
            atktimer -= Time.deltaTime;
        }


        if (atkanim == 1)
        {
            anim.SetBool("isAttack1", true);
        }
        else if (atkanim == 2)
        {
            anim.SetBool("isAttack2", true);
        }
    }
    public override void FinishAttack()
    {
        //atkanim = Random.Range(1, 2);

        if (atktimer > 0 || !Input.GetButton("Attack"))
        {
            anim.SetBool("isAttack1", false);
            anim.SetBool("isAttack2", false);
            atkanim = 0;
        }


        //anim.SetBool("isAttack2", false);
        //anim.SetFloat("afterAttack", 0);

    }

    public override void ApplyDamage()
    {
        // Collider2D[] hits = Physics2D.OverlapPointAll(atkPoint.position, eLayer);
        // float demeg = 0;
        // if (hits.Length > 0)
        // {
        //     enemyAttr = hits[0].gameObject.GetComponent<EnemyStat>();
        //     demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
        //     hits[0].GetComponent<EnemyHealth>().HealthChange(-demeg);
        // }
        float demeg;
        EnemyStat enemyAttr;



        typeIdx = (int)dmgBehaviour.dmgType;
        elemIdx = (int)dmgBehaviour.dmgElement;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(atkPoint.position, attr.atkRange, eLayer);
        if (enemies.Length > 0)
        {
            if (atkAmount < 3)
            {
                atkAmount += 1;
            }
            else if (atkAmount == 3)
            {
                if(activeCor == null)
                {
                    activeCor = StartCoroutine(IncreaseCrit());
                }
            }
            enemyAttr = enemies[0].gameObject.GetComponent<EnemyStat>();
            demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
            enemies[0].GetComponent<EnemyHealth>().HealthChange(-demeg);
            Debug.Log(demeg);
            isHit = true;

        }
    }

    public IEnumerator IncreaseCrit()
    {
        attr.critRate += 100f;
        attr.critDmg += cdmInc;
        yield return new WaitUntil(() => isHit);
        atkAmount = 0;
        attr.critRate -= 100f;
        attr.critDmg -= cdmInc;
        isHit = false;
        activeCor = null;
    }
    // public IEnumerator Cooldown()
    // {
    //     if (atktimer > 0)
    //     {
    //         atktimer -= Time.deltaTime;
    //     }
    //     else if (atktimer < 0)
    //     {

    //     }
    //     yield return new WaitUntil(() => atktimer == 0);
    // }




    // Update is called once per frame

}
