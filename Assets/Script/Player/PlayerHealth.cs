using UnityEngine;
using TMPro;
using NUnit.Framework.Constraints;


public class PlayerHealth : MonoBehaviour
{
    public PlayerAttribute attr;
    public float health;
    public bool isMaxHealth;
    public bool isDead;
    public TMP_Text healthText;
    public Animator txtAnim;
    public Animator anim;
    public PlayerCombat combat;
    public AudioSource aaudio;



    void Start()
    {
        attr = GetComponent<PlayerAttribute>();
        health = attr.maxHealth;
        healthText.text = "HP: " + health + " / " + attr.maxHealth;

    }

    public void HealthChange(float amount)
    {
        print(amount);
        if (combat.iFrame == true)
        {
            return;

        }
        else if (combat.iFrame == false)
        {
            if (anim.GetBool("isParry") == true)
            {
                aaudio.Play();
                int idx = GameUtils.ArrayChecker(combat.ParryCD, 0f);
                Debug.Log("Parry index: " + idx);
                combat.ParryCD[idx] -= amount / 6;
                if (combat.ParryCD[idx] >= attr.parryCap)
                {
                    combat.ParryCD[idx] = attr.parryCap;
                }
                else if (combat.ParryCD[idx] < attr.parryCap)
                {
                    combat.ParryCD[idx] -= amount / 6;
                }
            }
            else
            {
                txtAnim.Play("HPText");
                health += amount;


            }
        }

        healthText.text = "HP: " + health + " / " + attr.maxHealth;
        if (health <= 0)
        {
            healthText.text = "Awokwkwk Mokad" + "(HP: " + health + " / " + attr.maxHealth + ")";
            healthText.color = Color.red;
            gameObject.SetActive(false);
            isDead = true;
        }


    }


    // public void parryChecker()
    // {
    //     for (int i = 0; i < combat.ParryCD.Length; i++)
    //     {
    //         bool isLoop = true;
    //         if (combat.ParryCD[i] < 100000 && isLoop == true)
    //         {
    //             isLoop = false;
    //         }
    //     }
    // }

    private void FixedUpdate()
    {
        if (health == attr.maxHealth)
        {
            isMaxHealth = true;
        }
        else
        {
            isMaxHealth = false;
        }


    }

    void Update()
    {
        if (isDead == true || health <= 0)
        {
            healthText.color = Color.red;
        }
        else if (isMaxHealth == true)
        {
            healthText.color = Color.green;
        }
        else
        {
            healthText.color = Color.white;
        }
    }

}
