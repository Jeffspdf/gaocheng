using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    [Header("�������߲�λ")]
    public int passiveSlotLimit = 6;

    // ��ǰ���е���
    public List<ItemData> PassiveItems { get; private set; } = new List<ItemData>();
    public ItemData ActiveItem { get; private set; }

    private PlayerStats playerStats;

    private void Awake()
    {
        Instance = this;
        playerStats = GetComponent<PlayerStats>();
        LoadInventory();
    }

    // ʰȡ����
    public void PickupItem(ItemData item)
    {
        switch (item.type)
        {
            case ItemType.Passive:
                if (PassiveItems.Count < passiveSlotLimit)
                {
                    PassiveItems.Add(item);
                    item.ApplyEffect(playerStats);
                    SaveInventory();
                }
                break;
            case ItemType.Active:
                
                if (ActiveItem != null) ActiveItem.RemoveEffect(playerStats);
                ActiveItem = item;
                item.ApplyEffect(playerStats);
                SaveInventory();
                break;
        }

       
    }

    // ���ݳ־û�
    private void SaveInventory()
    {
        // ʵ��JSON���л������߼�
    }

    private void LoadInventory()
    {
        // ʵ�ּ����߼�
    }
}