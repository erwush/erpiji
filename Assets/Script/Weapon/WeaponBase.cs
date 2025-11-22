using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public abstract void Attack();

    public abstract void FinishAttack();

    public abstract void ApplyDamage();

}
