using UnityEngine;

public interface IDamageable
{
    public void Damage(HitInfo hitInfo);
}

public struct HitInfo
{
    public GameObject sender;
    public float damage;
    public DamageType damageType;

    public HitInfo(GameObject sender, float damage, DamageType damageType = DamageType.Flat)
    {
        this.sender = sender;
        this.damage = damage;
        this.damageType = damageType;
    }
}

public enum DamageType
{
    Flat,
    Current,
    Max
}