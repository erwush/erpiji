using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Tooltip("A temporary state where the player can't be damaged")]
    public bool iFrame = false;
    public bool isSkill1 = false;
    public float[] ParryCD = { 0f, 0f, 0f };
    public bool[] canParry = { true, true, true };
    public Transform atkPoint;
    public float cooldown;
    private float atktimer;
    public Animator anim;
    public float atkanim = 0;

    public bool isCharging;
    public Texture2D magicCursor;
    public PlayerAttribute attr;
    public DamageBehaviour dmgBehaviour;
    public int elemIdx;
    public int typeIdx;
    public WeaponBase weapon;
    public LayerMask eLayer;
    public EnemyStat enemyAttr;
    private Queue<(int, float duration)> cdQueue = new Queue<(int, float)>();
    // IEnumerator ParryCooldown()
    // {
    //     yield return new WaitUntil(() => parryCD);
    //     ParryCD[0] += Time.deltaTime;
    //     yield return new WaitUntil(() => ParryCD[0] >= 100000f);
    //     parryCD = false;
    // }

    private void Update()
    {
        if (Input.GetButton("Attack"))
        {
            weapon.Attack();
        }
        

        if (atktimer > 0)
        {
            atktimer -= Time.deltaTime; //time.deltatime = seberapa banyak frame yang bisa dijalankan komputeermu dalam 1 detik
        }

        // if(ParryCD[0] < 100000f)
        // {
        //     parryCD = true;
        // }
        if (ParryCD[0] > 0f)
        {
            ParryCD[0] -= Time.deltaTime;
            if (ParryCD[0] < 0f)
            {
                ParryCD[0] = 0f;
            }
        }

        if (ParryCD[1] > 0f)
        {
            ParryCD[1] -= Time.deltaTime;
            if (ParryCD[1] < 0f)
            {
                ParryCD[1] = 0f;
            }
        }
        if (ParryCD[2] > 0f)
        {
            ParryCD[2] -= Time.deltaTime;

            if (ParryCD[2] < 0f)
            {
                ParryCD[2] = 0f;
            }
        }



        //if(anim.GetBool("isAttack1") == true)
        //{
        //    atkanim = Random.Range(1,2);

        //}
    

    }

    private void FixedUpdate()
    {
        // GameUtils.DamageApplier(attr.atk, attr.def, attr.critRate, attr.critDmg, attr.dmgType[typeIdx], attr.dmgRes[typeIdx], attr.elemDmg[elemIdx], attr.elemRes[elemIdx], typeIdx);
    }

    private void Start()
    {
        weapon = GetComponent<WeaponBase>();
        anim = GetComponent<Animator>();
        attr = GetComponent<PlayerAttribute>();
        dmgBehaviour = GetComponent<DamageBehaviour>();
        anim.SetBool("isAttack1", false);
        anim.SetBool("isAttack2", false);
        atkanim = 0;
        cooldown = attr.atkSpd;
        // dmgBehaviour.dmgElement = (DamageElement)1;
    }

    public void FinishParry()
    {
        if (!Input.GetButton("Parry") || ParryCD[0] > 0f || ParryCD[1] > 0f || ParryCD[2] > 0f)
        {
            anim.SetBool("isParry", false);
        }
    }


    public void Parry()
    {
        if (ParryCD[0] <= 0f || ParryCD[1] <= 0f || ParryCD[2] <= 0f)
        {
            anim.SetBool("isParry", true);
        }

    }

    public void SpellCast()
    {

    }
    // [HideInInspector]public bool hasAttack = false;
    // public IEnumerator AttackCoroutine()
    // {

    //      hasAttack = true;
    //      //wait 0.1
    //     yield return new WaitForSeconds(0.1f);
    //     hasAttack = false;
    // }

    public bool hasHit;
    
    

    // public void Attack()
    // {
    //     // if (Input.GetButton("Attack"))
    //     // {
    //     //     float chargeTime = Time.deltaTime;
    //     //     if(chargeTime <= 0.8f)
    //     //     {
    //     //         isCharging = true;
    //     //     }
    //     //     else
    //     //     {
    //     //         isCharging = false;
    //     //     }
    //     // }
 
    //     if (atktimer <= 0)
    //     {
    //         // StartCoroutine(AttackCoroutine());

    //         //if (atkanim == 2)
    //         //{
    //         // anim.SetBool("isAttack1", true);
    //         atkanim = Random.Range(1, 3);


    //         //}
    //         //else if (atkanim == 2)
    //         //{
    //         //    anim.SetBool("isAttack2", true);
    //         //    atkanim = Random.Range(1, 2);
    //         //}

    //         atktimer = cooldown;

    //     }



    // }

   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atkPoint.position, attr.atkRange);
    }

    //public void Attack2()
    //{
    //    anim.SetBool("isAttack2", true);
    //}

   


    //public void FinishAttacking2()
    //{
    //    anim.SetBool("isAttack2", false);
    //    anim.SetBool("isAttack2", false);
    //    anim.SetFloat("afterAttack", 0);
    //}

}
