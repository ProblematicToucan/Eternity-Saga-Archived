using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "new Inventory", menuName = "GarammStudio/Items Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    public event UnityAction OnInventoryChanged;
    [field: SerializeField] public int capacity { get; private set; } = 50;
    public List<InventorySlot> inventorySlots;
    public bool IsFull() => inventorySlots.Count >= capacity;

    private void OnValidate()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item != null)
            {
                inventorySlots[i].itemID = inventorySlots[i].item.itemID;
            }
        }
    }

    public void AddItem(ItemSO _item, int _amount)
    {
        var hasItem = false;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item == _item)
            {
                inventorySlots[i].amount += _amount;
                hasItem = true;
                break;
            }
        }
        if (!hasItem && !IsFull())
        {
            inventorySlots.Add(new InventorySlot(_item, _amount));
        }
        OnInventoryChanged?.Invoke();
    }

    public void RegisterEvent()
    {
        Item.OnTouch += OnTouch;
    }

    public void UnregisterEvent()
    {
        Item.OnTouch -= OnTouch;
        inventorySlots.Clear();
    }

    private void OnTouch(ItemSO obj, int amount)
    {
        AddItem(obj, amount);
    }
}

/// <summary>
/// Contains itemID, itemSO and amount
/// </summary>
[System.Serializable]
public class InventorySlot
{
    public int itemID;
    public ItemSO item;
    public int amount;
    public InventorySlot()
    {
        itemID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(ItemSO _item)
    {
        itemID = _item.itemID;
        item = _item;
        amount = 1;
    }
    public InventorySlot(ItemSO _itemSO, int _amount)
    {
        itemID = _itemSO.itemID;
        item = _itemSO;
        amount = _amount;
    }
    public void AddAmount(int _amount)
    {
        amount += _amount;
    }
}