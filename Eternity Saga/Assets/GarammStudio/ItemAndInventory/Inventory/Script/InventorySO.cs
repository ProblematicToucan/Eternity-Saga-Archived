using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "GarammStudio/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private ItemDatabaseSO itemDatabase;
    [SerializeField] private List<InventorySlot> inventorySlots;
    public event Action OnInventoryChanged = delegate { };

    public List<InventorySlot> GetInventorySlots() => inventorySlots;

    public ItemDatabaseSO ItemDatabaseSO() => itemDatabase;

    public void AddItem(InventorySlot slot)
    {
        var stackable = itemDatabase.GetItemSO(slot.ItemID).IsStackable;
        var index = inventorySlots.FindIndex(inventorySlot => inventorySlot.ItemID == slot.ItemID && inventorySlot.ItemCount < 99);

        if (stackable && index >= 0)
        {
            var itemCount = inventorySlots[index].ItemCount + slot.ItemCount;
            if (itemCount > 99)
            {
                inventorySlots[index].ItemCount = 99;
                inventorySlots.Add(new InventorySlot { ItemID = slot.ItemID, ItemCount = itemCount - 99 });
            }
            else
            {
                inventorySlots[index].ItemCount = itemCount;
            }
        }
        else
        {
            inventorySlots.Add(slot);
        }

        OnInventoryChanged?.Invoke();
    }

    [ContextMenu("Add Sample")]
    private void AddSample()
    {
        AddItem(new InventorySlot { ItemID = 456727386, ItemCount = 1 });
        AddItem(new InventorySlot { ItemID = -1565546420, ItemCount = 3 });
    }
}

[System.Serializable]
public class InventorySlot
{
    public int ItemID;
    public int ItemCount;
}
