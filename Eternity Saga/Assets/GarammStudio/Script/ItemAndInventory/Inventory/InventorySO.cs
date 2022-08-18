using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Inventory", menuName = "GarammStudio/Items Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [field: SerializeField] public int capacity { get; private set; } = 50;
    public List<InventorySlot> container;
    public bool IsFull() => container.Count >= capacity;

    private void OnValidate()
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item != null)
            {
                container[i].itemID = container[i].item.itemID;
            }
        }
    }

    public void AddItem(ItemSO _item, int _amount)
    {
        var hasItem = false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == _item)
            {
                container[i].amount += _amount;
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            container.Add(new InventorySlot(_item, _amount));
        }
    }

    public void RegisterEvent()
    {
        Item.OnTouch += OnTouch;
    }

    public void UnregisterEvent()
    {
        Item.OnTouch -= OnTouch;
        container.Clear();
    }

    private void OnTouch(ItemSO obj, int amount)
    {
        AddItem(obj, amount);
    }
}

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