using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ScriptableObject that handle inventory functionality.
/// </summary>
[CreateAssetMenu(fileName = "new Inventory", menuName = "GarammStudio/Items Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    public event UnityAction OnInventoryChanged;
    [field: SerializeField] public ItemDatabaseSO ItemDatabase { get; private set; }
    [field: SerializeField] public int Capacity { get; private set; } = 50;
    public List<InventorySlot> inventorySlots;
    public bool IsFull() => inventorySlots.Count >= Capacity;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Auto validate Items id on inventory list with items id that registered on Scriptableobject.
        for (var i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].Item != null)
            {
                inventorySlots[i].ItemId = inventorySlots[i].Item.Id;
            }
        }
    }
#endif

    /// <summary>
    /// Add item to the inventory functionality.
    /// </summary>
    /// <param name="item">Items to be added to inventory.</param>
    /// <param name="amount">Items amount to be added to inventory.</param>
    public void AddItem(Item item, int amount)
    {
        var hasItem = false;
        var itemOnDatabase = ItemDatabase.GetItemSOReferenceById(item.Id);
        if (itemOnDatabase == null) return;
        // If items to be added to inventory holds buff, dont stack it.
        if (item.Buffs.Length > 0)
        {
            inventorySlots.Add(new InventorySlot(item, amount));
            SortSlot(); // sort the list.
            OnInventoryChanged?.Invoke(); // Tell a subscriber that we have some data changes on inventory.
            return;
        }
        // If item don't holds buff and...
        for (var i = 0; i < inventorySlots.Count; i++)
        {
            // we have item on inventory list, just add amount of item.
            if (inventorySlots[i].Item.Id == item.Id)
            {
                inventorySlots[i].Amount += amount;
                hasItem = true;
                break;
            }
        }
        // we don't have item on inventory list, just add the item to the list.
        if (!hasItem && !IsFull())
        {
            inventorySlots.Add(new InventorySlot(item, amount));
        }
        SortSlot(); // sort the list.
        OnInventoryChanged?.Invoke(); // Tell a subscriber that we have some data changes on inventory.
    }

    /// <summary>
    /// Remove item from inventory.
    /// </summary>
    /// <param name="inventorySlot">Slot that want to remove.</param>
    /// <param name="amount">Item amount that want to remove.</param>
    public void RemoveItem(InventorySlot inventorySlot, int amount)
    {
        var index = inventorySlots.IndexOf(inventorySlot); // Find index of the inventory slot.
        inventorySlots[index].Amount -= amount; // Reduce amount of the item.
        // If amount <= 0, we delete the slot.
        if (inventorySlots[index].Amount <= 0)
        {
            inventorySlots.Remove(inventorySlot);
            SortSlot();
        }
        OnInventoryChanged?.Invoke();
    }

    /// <summary>
    /// Subscribe OnTouch event to AddItem.
    /// </summary>
    public void RegisterEvent()
    {
        ItemDrop.OnTouch += AddItem;
    }

    /// <summary>
    /// UnSubscribe OnTouch event to AddItem.
    /// </summary>
    public void UnRegisterEvent()
    {
        ItemDrop.OnTouch -= AddItem;
        inventorySlots.Clear();
    }

    /// <summary>
    /// Save inventory list to file.
    /// </summary>
    [ContextMenu("Save")]
    public void Save()
    {
        var savesData = new List<InventorySaveData>();
        var baseSavePath = Application.persistentDataPath;
        var saveFile = Path.Combine(baseSavePath, this.name);
        for (var i = 0; i < inventorySlots.Count; i++)
        {
            savesData.Add(new InventorySaveData(inventorySlots[i].ItemId, inventorySlots[i].Amount, inventorySlots[i].Item.Buffs));
        }
        FileReadWrite.WriteToBinaryFile(saveFile + ".dat", savesData);
    }

    /// <summary>
    /// Load inventory list from file.
    /// </summary>
    [ContextMenu("Load")]
    public void Load()
    {
        var baseSavePath = Application.persistentDataPath;
        var saveFile = Path.Combine(baseSavePath, this.name);
        var savesData = FileReadWrite.ReadFromBinaryFile<List<InventorySaveData>>(saveFile + ".dat");
        inventorySlots.Clear();
        for (var i = 0; i < savesData.Count; i++)
        {
            AddItem(new Item(ItemDatabase.GetItemSOReferenceById(savesData[i].ItemId), savesData[i].Buffs), savesData[i].Amount);
        }
    }

    /// <summary>
    /// Sort the inventory list.
    /// </summary>
    [ContextMenu("Sort")]
    public void SortSlot()
    {
        var slotTypeComparer = new SlotTypeComparer(ItemDatabase);
        var slotNameComparer = new SlotNameComparer(ItemDatabase);
        inventorySlots.Sort(slotNameComparer); // sort list by name.
        inventorySlots.Sort(slotTypeComparer); // sort list by type.
    }

    /// <summary>
    /// Add empty slot with defined empty item.
    /// </summary>
    [ContextMenu("Add Empty Slot")]
    public void AddEmptySlot()
    {
        inventorySlots.Add(new InventorySlot());
    }
}

/// <summary>
/// Class slot item on inventory that contains itemID, itemSO and amount.
/// </summary>
[Serializable]
public class InventorySlot
{
    /// <summary>
    /// The delegate to call if the data's selection state
    /// has changed. This will update any views that are hooked
    /// to the data so that they show the proper selection state UI.
    /// </summary>
    public SelectedChangedDelegate selectedChanged;
    /// <summary>
    /// The selection state
    /// </summary>
    private bool _selected;
    public int ItemId;
    public Item Item;
    public int Amount;

    /// <summary>
    /// Constructor InventorySlot.
    /// </summary>
    public InventorySlot()
    {
        ItemId = -1;
        Item = null;
        Amount = 0;
    }

    /// <summary>
    /// Constructor InventorySlot with ItemSO and Amount.
    /// </summary>
    /// <param name="itemSo">itemSO</param>
    /// <param name="amount">item amount</param>
    public InventorySlot(Item itemSo, int amount)
    {
        ItemId = itemSo.Id;
        Item = itemSo;
        Amount = amount;
    }

    public bool Selected
    {
        get { return _selected; }
        set
        {
            // if the value has changed
            if (_selected != value)
            {
                // update the state and call the selection handler if it exists
                _selected = value;
                selectedChanged?.Invoke(_selected);
            }
        }
    }
}

/// <summary>
/// Class that represent save data format.
/// </summary>
[Serializable]
public class InventorySaveData
{
    /// <summary>
    /// Id of the item that will be to save.
    /// </summary>
    public int ItemId;
    /// <summary>
    /// Amount of the item that will be to save.
    /// </summary>
    public int Amount;
    /// <summary>
    /// Item buffs that will be to save.
    /// </summary>
    public ItemBuff[] Buffs;
    /// <summary>
    /// Inventory Save Data.
    /// </summary>
    /// <param name="itemId">Id of the item that we want to save.</param>
    /// <param name="amount">Amount of the item that we want to save.</param>
    /// <param name="buffs">Item buffs that we want to save.</param>
    public InventorySaveData(int itemId, int amount, ItemBuff[] buffs)
    {
        ItemId = itemId;
        Amount = amount;
        Buffs = buffs;
    }
}

/// <summary>
/// Comparer class list by item type.
/// </summary>
internal class SlotTypeComparer : IComparer<InventorySlot>
{
    /// <summary>
    /// Database that hold all item and functionality get itemSO using item id.
    /// </summary>
    private readonly ItemDatabaseSO _databaseSo;

    /// <summary>
    /// Comparer to sort list by item type.
    /// </summary>
    /// <param name="databaseSO">Database.</param>
    public SlotTypeComparer(ItemDatabaseSO databaseSO)
    {
        _databaseSo = databaseSO;
    }
    public int Compare(InventorySlot x, InventorySlot y)
    {
        return _databaseSo.GetItemSOReferenceById(x!.ItemId).ItemType.CompareTo(
            _databaseSo.GetItemSOReferenceById(y!.ItemId).ItemType
        );
    }
}

/// <summary>
/// Comparer class list by item name.
/// </summary>
internal class SlotNameComparer : IComparer<InventorySlot>
{
    /// <summary>
    /// Database that hold all item and functionality get itemSO using item id.
    /// </summary>
    private readonly ItemDatabaseSO _databaseSo;

    /// <summary>
    /// Comparer to sort list by item name.
    /// </summary>
    /// <param name="databaseSO">Database.</param>
    public SlotNameComparer(ItemDatabaseSO databaseSO)
    {
        _databaseSo = databaseSO;
    }
    public int Compare(InventorySlot x, InventorySlot y)
    {
        return string.Compare(
            _databaseSo.GetItemSOReferenceById(x!.ItemId).ItemName, _databaseSo.GetItemSOReferenceById(y!.ItemId).ItemName, StringComparison.Ordinal);
    }
}

/// <summary>
/// This delegate handles any changes to the selection state of the data
/// </summary>
/// <param name="val">The state of the selection</param>
public delegate void SelectedChangedDelegate(bool val);