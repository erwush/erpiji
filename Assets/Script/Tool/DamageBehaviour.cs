using UnityEngine;

public class DamageBehaviour : MonoBehaviour
{
    public DamageType dmgType;
    public DamageElement dmgElement;


}
public enum DamageType
{
    Physical = 0,
    Magic = 1,
    True = 2
}

public enum DamageElement
{
    None = 0,
    Fire = 1,
    Water = 2,
    Earth = 3,
    Wind = 4,
    Lightning = 5
}


public enum DamageAbility
{
    None = 0,
    BasicAttack = 1,
    Skill = 2,
    Debuff = 3,
    Artifact = 4
}