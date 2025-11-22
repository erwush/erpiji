using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Scriptable Objects/Spell")]
public class Spell : ScriptableObject
{
    public string spellName;
    public int spellId;
    public Sprite icon;
    public GameObject spellPrefab;
    public float manaCost;
    public float cooldown;
    public float castTime;
    public float spellPower;

    public Spell spellType;

    public Element spellElement;

}

public enum SpellType
{
    Offensive = 0,
    Defensive = 1
}


public enum Element
{
    Fire = 0,
    Water = 1,
    Earth = 2,
    Wind = 3,
    Lightning = 4
}