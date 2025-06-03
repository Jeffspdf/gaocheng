using UnityEngine;

public enum ItemType { Passive, Active, Consumable }

public abstract class ItemData : ScriptableObject
{
    [Header("��������")]
    public string itemName;
    public Sprite icon;
    public ItemType type;
    [TextArea] public string description;

    // Ӧ��Ч��
    public abstract void ApplyEffect(PlayerStats stats);

    // �Ƴ�Ч��
    public virtual void RemoveEffect(PlayerStats stats) { }
}