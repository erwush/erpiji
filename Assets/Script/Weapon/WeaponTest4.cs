//*CHAINSAW TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest4 : WeaponBase
{


    public Animator anim;
    public float atkanim;
    public float atktimer;
    private SpriteRenderer sprite;
    public BuffManager buffController;
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
        buffController = GameObject.FindWithTag("GameController").GetComponent<BuffManager>();
    }

    public override void Attack()
    {

        if (atktimer <= 0)
        {
            atkanim = Random.Range(1, 3);

            atktimer = 1 / attr.atkSpd;

        }
    }
    public bool isHolding;

    public override void ApplyDamage()
    {
        if (!isHolding)
            StartCoroutine(AttackHold());


    }

    public IEnumerator AttackHold()
    {
        isHolding = true;

        // Freeze animasi di frame hit
        anim.speed = 0f;
        float interval = 1f / attr.atkSpd;

        // interval damage (bisa dikaitkan ke attack speed)


        while (Input.GetButton("Attack"))
        {
            Base_ApplyDamage();     // AOE / chain / burn dll

            // TUNGGU → penting biar tidak spam per frame
            yield return new WaitForSeconds(interval);
        }

        // Lepas tombol → lanjutkan animasi
        anim.speed = 1f;
        isHolding = false;
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
                if (enemyAttr == null)
                {
                    Debug.Log("enemy = null");
                    continue;
                }


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

                enemy.GetComponent<EnemyHealth>().HealthChange(-demeg);

                if (buffController.activeBless.Contains("Lifesteal"))
                {
                    StartCoroutine(GetHealth(demeg));
                }

                if (buffController.activeBless.Contains("Burn"))
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
                }

                if (buffController.activeBless.Contains("CritHit"))
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
                }

                if (buffController.activeBless.Contains("ChainLightning"))
                {
                    Transform firstTarget = enemies[0].transform;
                    Debug.Log("chain1");
                    StartCoroutine(TriggerChain(enemy));
                }
            }


        }
    }

    //*CHAIN LIGHTNING
    public float maxChain;
    public float chainRadius;
    public float chainDmg;

    public IEnumerator TriggerChain(Collider2D currentTarget)
    {
        List<Collider2D> hitTargets = new List<Collider2D>();

        Collider2D lastTarget = currentTarget;
        for (int i = 0; i < maxChain; i++)
        {
            Debug.Log("chaini" + i);
                        if (lastTarget == null) break;
            ChainDamage(lastTarget);
            hitTargets.Add(lastTarget);
            yield return new WaitForSeconds(0.1f); // d

            Collider2D[] nearby = Physics2D.OverlapCircleAll(
                lastTarget.transform.position,
                chainRadius,
                eLayer
            );

            Collider2D nextTarget = null;
            float closestDist = Mathf.Infinity;
            foreach (Collider2D col in nearby)
            {
                if (!hitTargets.Contains(col))
                {
                    float dist = Vector2.Distance(lastTarget.transform.position, col.transform.position);
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        nextTarget = col;
                    }
                }
            }

            if (nextTarget == null)
            {
                break;
            }

            SpawnLightningEffect(lastTarget.transform.position, nextTarget.transform.position);

            // lanjut ke target berikutnya
            lastTarget = nextTarget;
            Debug.Log("chain2");

        }
    }

    public void ChainDamage(Collider2D enemy)
    {
        EnemyStat enemyAttr = enemy.GetComponent<EnemyStat>();
        if (enemyAttr == null)
        {
            Debug.Log("enemy = null");
            return;
        }

        float demeg = GameUtils.DamageApplier(
                   attr.atk,
                   enemyAttr.def,
                   attr.dmgType[1],
                   enemyAttr.dmgRes[1],
                   attr.elemDmg[5],
                   enemyAttr.elemRes[5],
                   attr.critRate,
                   attr.critDmg,
                   typeIdx
               );

        enemy.GetComponent<EnemyHealth>().HealthChange(-demeg);
        Debug.Log("chaindemeg"+demeg);

    }

    private void SpawnLightningEffect(Vector2 from, Vector2 to)
    {
        // DIISI kalau kamu ada VFX line renderer atau prefab
        Debug.DrawLine(from, to, Color.cyan, 0.15f);
        Debug.Log("chainline");
    }











    //*LIFESTEAL
    public float Lifesteal_healPercent;
    // public void Lifesteal_ApplyDamage()

    // {

    //     // Collider2D[] hits = Physics2D.OverlapPointAll(atkPoint.position, eLayer);
    //     // float demeg = 0;
    //     // if (hits.Length > 0)
    //     // {
    //     //     enemyAttr = hits[0].gameObject.GetComponent<EnemyStat>();
    //     //     demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
    //     //     hits[0].GetComponent<EnemyHealth>().HealthChange(-demeg);
    //     // }



    //     typeIdx = (int)dmgBehaviour.dmgType;
    //     elemIdx = (int)dmgBehaviour.dmgElement;

    //     Collider2D[] enemies = Physics2D.OverlapCircleAll(atkPoint.position, attr.atkRange, eLayer);
    //     if (enemies.Length > 0)
    //     {

    //         foreach (Collider2D enemy in enemies)
    //         {

    //             EnemyStat enemyAttr = enemy.GetComponent<EnemyStat>();
    //             if (enemyAttr == null)
    //             {
    //                 Debug.Log("enemy = null");
    //                 continue;
    //             }
    //             float demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
    //             enemy.GetComponent<EnemyHealth>().HealthChange(-demeg);
    //             StartCoroutine(GetHealth(demeg));
    //         }


    //     }
    // }

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

    // public void CritHit_ApplyDamage()
    // {
    //     // Collider2D[] hits = Physics2D.OverlapPointAll(atkPoint.position, eLayer);
    //     // float demeg = 0;
    //     // if (hits.Length > 0)
    //     // {
    //     //     enemyAttr = hits[0].gameObject.GetComponent<EnemyStat>();
    //     //     demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
    //     //     hits[0].GetComponent<EnemyHealth>().HealthChange(-demeg);
    //     // }




    //     typeIdx = (int)dmgBehaviour.dmgType;
    //     elemIdx = (int)dmgBehaviour.dmgElement;

    //     Collider2D[] enemies = Physics2D.OverlapCircleAll(atkPoint.position, attr.atkRange, eLayer);
    //     if (enemies.Length > 0)
    //     {
    //         if (CritHit_atkAmount < 3)
    //         {
    //             CritHit_atkAmount += 1;
    //         }
    //         else if (CritHit_atkAmount == 3)
    //         {
    //             if (CritHit_activeCor == null)
    //             {
    //                 CritHit_activeCor = StartCoroutine(CritHit_IncreaseCrit());
    //             }
    //         }
    //         CritHit_isHit = true;
    //         foreach (Collider2D enemy in enemies)
    //         {
    //             EnemyStat enemyAttr = enemy.GetComponent<EnemyStat>();
    //             if (enemyAttr == null)
    //             {
    //                 Debug.Log("enemy = null");
    //                 continue;
    //             }
    //             float demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
    //             enemy.GetComponent<EnemyHealth>().HealthChange(-demeg);
    //             Debug.Log(demeg);

    //         }


    //     }
    // }

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

    //     public void Burning_ApplyDamage()
    //     {
    //         // Collider2D[] hits = Physics2D.OverlapPointAll(atkPoint.position, eLayer);
    //         // float demeg = 0;
    //         // if (hits.Length > 0)
    //         // {
    //         //     enemyAttr = hits[0].gameObject.GetComponent<EnemyStat>();
    //         //     demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
    //         //     hits[0].GetComponent<EnemyHealth>().HealthChange(-demeg);
    //         // }


    //         typeIdx = (int)dmgBehaviour.dmgType;
    //         elemIdx = (int)dmgBehaviour.dmgElement;

    //         Collider2D[] enemies = Physics2D.OverlapCircleAll(atkPoint.position, attr.atkRange, eLayer);
    //         if (enemies.Length > 0)
    //         {
    //             foreach (Collider2D enemy in enemies)
    //             {
    //                 EnemyStat enemyAttr = enemy.GetComponent<EnemyStat>();
    //                 if (enemyAttr == null)
    //                 {
    //                     Debug.Log("enemy = null");
    //                     continue;
    //                 }
    //                 EnemyBurning burn = enemy.GetComponent<EnemyBurning>();
    //                 if (burn == null)
    //                 {
    //                     burn = enemy.gameObject.AddComponent<EnemyBurning>();
    //                     burn.burnDmg = Burning_burnDmg;
    //                     burn.burnDelay = Burning_delay;
    //                     burn.burnDuration = Burning_burnDuration;
    //                     burn.isBurning = true;
    //                 }

    //                 float demeg = GameUtils.DamageApplier(attr.atk, enemyAttr.def, attr.dmgType[typeIdx], enemyAttr.dmgRes[typeIdx], attr.elemDmg[elemIdx], enemyAttr.elemRes[elemIdx], attr.critRate, attr.critDmg, typeIdx);
    //                 enemy.GetComponent<EnemyHealth>().HealthChange(-demeg);
    //             }


    //         }
    //     }
}
