using UnityEngine;

[System.Serializable]
public struct CooldownData
{
    public int index;
    public float duration;

    public CooldownData(int id, float dr)
    {
        index = id;
        duration = dr;
    }
}
