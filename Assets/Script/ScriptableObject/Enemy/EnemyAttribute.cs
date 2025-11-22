using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class EnemyAttribute : ScriptableObject
{
[Header("Basic Attribute")]

    [Tooltip("Enemy's Current Health")]
    public float health = 100;
    [Tooltip("Enemy's Max Health")]
    public float maxHealth = 100;
    [Tooltip("Enemy's Default Health (100)")]
    public float defaultHealth = 100;
    [Tooltip("Each 1 defense reduce 0.1 damage")]
    public float def = 5;
    [Tooltip("Enemy's Attack (10 by default and its not Attack Damage, just Attack)")]
    public float atk = 10;
    [Tooltip("Enemy's Attack Speed (1 by default)")]
    public float atkSpd = 1.15f;
    [Tooltip("Enemy's Speed (10 by default)")]
    public float spd = 10f;

    [Header("Advanced Attribute")]
    public float knockback;
    public float atkRange = 2f;



    [Header("Elemental Attribute")]
    [Tooltip("Damage Type (100% by default)")]
    public float[] dmgType = { 100f, 100f, 100f }; //0 = Physical 1 = Magic 2 = True
    public float[] dmgRes = {0f, 0f, 0f}; //0 = Physical 1 = Magic 2 = True
    [Tooltip("Elemental Damage (100% by default)")]
    public float[] elemDmg = { 100f, 100f, 100f, 100f, 100f, 100f }; //Fire, Water, Earth, Wind, Lightning
    [Tooltip("Elemental Resistance (0% by default)")]
    public float[] elemRes = { 0f,0f, 0f, 0f, 0f, 0f }; //Fire, Water, Earth, Wind, Lightning


    [Header("Status Effect Attribute")]
    [Tooltip("The duration of the debuff applied to the Enemy (100% by default)")]
    public float debuffDuration = 100f;
    [Tooltip("The duration of the buff applied to the Enemy (100% by default)")]
    public float buffDuration = 100f;
    [Tooltip("The amount of health healed the Enemy will receive when being healed(100% by default)")]
    public float healAmount = 100f;
    [Tooltip("The resistance of the debuff applied to the Enemy (0% by default)")]
    public float debuffRes = 0f;
    [Tooltip("The additional chance for certain effect to happen (0% by default)")]
    public float addChance = 0f;

}
