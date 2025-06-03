using UnityEngine;
public class PassiveItem : ItemData
{
    private PlayerStats stats;
    [Header("���Լӳ�")]
    public float healthBoost = 0f;
    public float speedBoost = 0f;
    public float damageBoost = 0f;

    public override void ApplyEffect(PlayerStats stats)
    {
        stats.MaxHP += healthBoost;
        stats.CurrentHP += healthBoost; // ������Ѫ
        stats.MoveSpeed *= (1 + speedBoost);
        stats.AttackPower *= (1 + damageBoost);
    }

    public override void RemoveEffect(PlayerStats stats)
    {
        stats.MaxHP -= healthBoost;
        stats.MoveSpeed /= (1 + speedBoost);
        stats.AttackPower /= (1 + damageBoost);
    }
}