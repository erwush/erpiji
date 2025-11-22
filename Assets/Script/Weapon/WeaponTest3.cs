//*LIFESTEAL TEST
using System.Collections;
using UnityEngine;

public class WeaponTest3 : WeaponBase
{


    public Animator anim;
    public float atkanim;
    public float atktimer;
    private SpriteRenderer sprite;
    public PlayerCombat combat;
    public PlayerAttribute attr;
    public DamageBehaviour dmgBehaviour;
    public PlayerHealth healthScr;
    public bool hasHit;
    public float cooldown;
    public int typeIdx;
    public int elemIdx;
    public Transform atkPoint;
    public LayerMask eLayer;

    //UNIQUE VARIABLE
    public enum ActiveBlessing
    {
        None = 0,
        Lifesteal = 1,
        CritHit = 2,
        Burn = 3,
    }

    public ActiveBlessing bless;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
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

            atktimer = 1 / attr.atkSpd;

        }
    }

    public override void ApplyDamage()
    {
        if (bless == ActiveBlessing.None)
        {
            Base_ApplyDamage();
        }
        else if (bless == ActiveBlessing.Lifesteal)
        {
            Lifesteal_ApplyDamage();
        }
        else if (bless == ActiveBlessing.CritHit)
        {
            CritHit_ApplyDamage();
        }
        else if (bless == ActiveBlessing.Burn)
        {
            Burning_ApplyDamage();
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

    //*BASE
    public void Base_ApplyDamage()
    {
        // Collider2D[] hits = Physics2D.OverlapPointAll(atkPoint.position, eLayer);
        // float demeg = 0;
        // if (hits.Length > 0)
        // {
        //     enemyAttr = hits[0].gameObject.GetComponent<EnemyStat>();
        //     demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
        //     hits[0].GetComponent<EnemyHealth>().HealthChange(-demeg);
        // }




        typeIdx = (int)dmgBehaviour.dmgType;
        elemIdx = (int)dmgBehaviour.dmgElement;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(atkPoint.position, attr.atkRange, eLayer);
        if (enemies.Length > 0)
        {
            foreach (Collider2D enemy in enemies)
            {
                EnemyStat enemyAttr = enemy.GetComponent<EnemyStat>();
                if (enemyAttr == null) continue;

                float demeg = GameUtils.DamageApplier(
                    attr.atk,
                    enemyAttr.def,
                    attr.dmgType[typeIdx],
                    enemyAttr.dmgRes[typeIdx],
                    attr.elemDmg[elemIdx],
                    enemyAttr.elemRes[elemIdx],
                    attr.critRate,
                    attr.critDmg,
                    typeIdx
                );

                enemy.GetComponent<EnemyHealth>()?.HealthChange(-demeg);

                Debug.Log("Damage ke " + enemy.name + ": " + demeg);
            }


        }
    }








    //*LIFESTEAL
    public float Lifesteal_healPercent;
    public void Lifesteal_ApplyDamage()
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
            foreach (Collider2D enemy in enemies)
            {
                enemyAttr = enemies[0].gameObject.GetComponent<EnemyStat>();
                demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
                enemies[0].GetComponent<EnemyHealth>().HealthChange(-demeg);
                StartCoroutine(GetHealth(demeg));
            }


        }
    }

    public IEnumerator GetHealth(float demeg)
    {

        float amount = (demeg * (Lifesteal_healPercent / 100)) * (attr.regenAmount[0] / 100);
        Debug.Log("healed: " + amount);

        healthScr.HealthChange(amount);
        sprite.color = Color.green;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;

    }





    //*CRITHIT
    public float CritHit_atkAmount;
    public float CritHit_cdmInc;
    public bool CritHit_isHit;
    public Coroutine CritHit_activeCor;

    public void CritHit_ApplyDamage()
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
            if (CritHit_atkAmount < 3)
            {
                CritHit_atkAmount += 1;
            }
            else if (CritHit_atkAmount == 3)
            {
                if (CritHit_activeCor == null)
                {
                    CritHit_activeCor = StartCoroutine(CritHit_IncreaseCrit());
                }
            }
            CritHit_isHit = true;
            foreach (Collider2D enemy in enemies)
            {
                enemyAttr = enemies[0].gameObject.GetComponent<EnemyStat>();
                demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
                enemies[0].GetComponent<EnemyHealth>().HealthChange(-demeg);
                Debug.Log(demeg);

            }


        }
    }

    public IEnumerator CritHit_IncreaseCrit()
    {
        attr.critRate += 100f;
        attr.critDmg += CritHit_cdmInc;
        yield return new WaitUntil(() => CritHit_isHit);
        CritHit_atkAmount = 0;
        attr.critRate -= 100f;
        attr.critDmg -= CritHit_cdmInc;
        CritHit_isHit = false;
        CritHit_activeCor = null;
    }







    //*BURNINGSWORD
    public float Burning_burnDmg;
    public float Burning_burnDuration;
    public float Burning_delay;

    public void Burning_ApplyDamage()
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
            foreach (Collider2D enemy in enemies)
            {
                EnemyBurning burn = enemy.GetComponent<EnemyBurning>();
                if (burn == null)
                {
                    burn = enemy.gameObject.AddComponent<EnemyBurning>();
                    burn.burnDmg = Burning_burnDmg;
                    burn.burnDelay = Burning_delay;
                    burn.burnDuration = Burning_burnDuration;
                    burn.isBurning = true;
                }
                enemyAttr = enemies[0].gameObject.GetComponent<EnemyStat>();
                demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
                enemies[0].GetComponent<EnemyHealth>().HealthChange(-demeg);
            }


        }
    }
}
