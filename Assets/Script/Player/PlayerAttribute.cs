using UnityEditor.EditorTools;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttribute : MonoBehaviour

{






    private Dictionary<string, System.Action<float>> setters;

    void Awake()
    {
        setters = new Dictionary<string, System.Action<float>>()
        {
            { "maxhp",  (value) => maxHealth += value },
            { "atk",  (value) => atk *= 1 + (value / 100f)},
            { "atkspd",   (value) => atkSpd  *= 1 + (value / 100f) },
            { "spd",  (value) => spd *= 1 + (value / 100f) },
            { "critrate",  (value) => critRate += value },
            { "critdmg",  (value) => critDmg += value },
            { "def",  (value) => def *= 1 + (value / 100f) },
        };

    }

    public void IncreaseAttr(string attributeName, float amount)
    {
        Debug.Log("Increased");
        if (setters.ContainsKey(attributeName))
            setters[attributeName](amount);
        else
            Debug.LogWarning("Attribute tidak ditemukan: " + attributeName);
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Basic Attribute")]

    [Tooltip("Player's Max Health")]
    public float maxHealth = 100;
    [Tooltip("Player's Default Health (100)")]
    public float defaultHealth = 100;
    [Tooltip("Player's Current Mana")]
    public float mana = 100;
    [Tooltip("Player's Max Mana")]
    public float maxMana = 100;
    [Tooltip("Player's Default Mana (100)")]
    public float defaultMana = 100;
    [Tooltip("Each 1 defense reduce 0.1 damage")]
    public float def = 5;
    [Tooltip("Player's Attack (10 by default and its not Attack Damage, just Attack)")]
    public float atk = 10f;
    [Tooltip("Player's Attack Speed (1 by default)")]
    public float atkSpd = 1.15f;
    [Tooltip("Player's Speed (10 by default)")]
    public float spd = 10f;
    [Tooltip("Player's chance to deal Critical Damage (5% by default)")]
    public float critRate = 5f;
    [Tooltip("Player's damage when dealing Critical Damage (dmg*critDmg (100% by default))")]
    public float critDmg = 100f;

    [Header("Advanced Attribute")]
    public float lifeSteal = 0f;
    public float knockback = 0f;
    public float atkRange = 2f;

    [Header("Ability Attribute")]
    public float[] ablDmg = { 100f, 100f, 100f, 100f }; //0 = None 1 = Basic 2 = Skill 3 = Debuff 4 = Artifact
    public float[] coolDown = { 100f, 100f, 100f, 100f, 100f }; //0 = Empty 1 = Empty` 2 = Skill 3 = Empty 4 = Artifact 5 = Parry


    [Header("Elemental Attribute")]
    [Tooltip("Damage Type (100% by default)")]
    public float[] dmgType = { 100f, 100f, 0f }; //0 = Physical 1 = Magic 2 = True
    public float[] dmgRes = { 0f, 0f, 0f }; //0 = Physical 1 = Magic 2 = True
    [Tooltip("Elemental Damage (100% by default)")]
    public float[] elemDmg = { 100f, 100f, 100f, 100f, 100f, 100f }; //None, Fire, Water, Earth, Wind, Lightning
    [Tooltip("Elemental Resistance (0% by default)")]
    public float[] elemRes = { 0f, 0f, 0f, 0f, 0f, 0f }; //None, Fire, Water, Earth, Wind, Lightning



    [Header("Status Effect Attribute")]
    [Tooltip("The duration of the debuff applied to the player (100% by default)")]
    public float debuffDuration = 100f;
    [Tooltip("The duration of the buff applied to the player (100% by default)")]
    public float buffDuration = 100f;
    [Tooltip("The duration and damage(if its a Damage Over Time/DoT) of the debuff applied by the player")]
    public float debuffPower = 100f;
    [Tooltip("The amount of health/mana healed the player will receive when being healed(100% by default)")]
    public float[] regenAmount = { 100f, 100f }; //1 = health 2 = mana
    [Tooltip("The chance to resist the debuff applied to the player (0% by default)")]
    public float debuffRes = 0f;
    [Tooltip("The additional chance for certain effect to happen (0% by default)")]
    public float addChance = 0f;

    [Header("Misc Attribute")]
    public float[] statCap = { 0f, 0f }; //0 = MaxHealth, 2= CritDMG
    public float parryCap = 45f;

    [Header("Non-Combat Attribute")]
    public float expBoost = 0f;
}





//past code
//Elemental Damage
// [Tooltip("Fire Elemental Damage (100% by default)")]
// public float fireDmg = 100f; //100% by default
// [Tooltip("Water Elemental Damage (100% by default)")]
// public float waterDmg = 100f; //100% by default
// [Tooltip("Earth Elemental Damage (100% by default)")]
// public float earthDmg = 100f; //100% by default
// [Tooltip("Wind Elemental Damage (100% by default)")]
// public float windDmg = 100f; //100% by default
// [Tooltip("Lightning Elemental Damage (100% by default)")]
// public float lightningDmg = 100f; //100% by default

// //Elemental Resistance
// [Tooltip("Fire Elemental Resistance (0% by default)")]
// public float fireRes = 0f; //0% by default
// [Tooltip("Water Elemental Resistance (0% by default)")]
// public float waterRes = 0f; //0% by default
// [Tooltip("Earth Elemental Resistance (0% by default)")]
// public float earthRes = 0f; //0% by default
// [Tooltip("Wind Elemental Resistance (0% by default)")]
// public float windRes = 0f; //0% by default
// [Tooltip("Lightning Elemental Resistance (0% by default)")]
// public float lightningRes = 0f; //0% by default