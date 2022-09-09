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
        // Auto validate Items id on inventory list with items id that registered on scriptableobject.
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].Item != null)
            {
                inventorySlots[i].ItemID = inventorySlots[i].Item.Id;
            }
        }
    }
#endif

    /// <summary>
    /// Add item to the inventory functionality.
    /// </summary>
    /// <param name="_item">Items to be added to inventory.</param>
    /// <param name="_amount">Items amount to be added to inventory.</param>
    public void AddItem(Item _item, int _amount)
    {
        var hasItem = false;
        var itemOnDatabase = ItemDatabase.GetItemSOReferenceById(_item.Id);
        if (itemOnDatabase == null) return;
        // If items to be added to inventory holds buff, dont stack it.
        if (_item.Buffs.Length > 0)
        {
            inventorySlots.Add(new InventorySlot(_item, _amount));
            SortSlot(); // sort the list.
            OnInventoryChanged?.Invoke(); // Tell a subscriber that we have some data changes on inventory.
            return;
        }
        // If item dont holds buff and...
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            // we have item on inventory list, just add amount of item.
            if (inventorySlots[i].Item.Id == _item.Id)
            {
                inventorySlots[i].Amount += _amount;
                hasItem = true;
                break;
            }
        }
        // we dont have item on inventory list, just add the item to the list.
        if (!hasItem && !IsFull())
        {
            inventorySlots.Add(new InventorySlot(_item, _amount));
        }
        SortSlot(); // sort the list.
        OnInventoryChanged?.Invoke(); // Tell a subscriber that we have some data changes on inventory.
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
        var saveDatas = new List<InventorySaveData>();
        var baseSavePath = Application.persistentDataPath;
        var saveFile = Path.Combine(baseSavePath, this.name);
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            saveDatas.Add(new InventorySaveData(inventorySlots[i].ItemID, inventorySlots[i].Amount, inventorySlots[i].Item.Buffs));
        }
        FileReadWrite.WriteToBinaryFile(saveFile + ".dat", saveDatas);
    }

    /// <summary>
    /// Load inventory list from file.
    /// </summary>
    [ContextMenu("Load")]
    public void Load()
    {
        var baseSavePath = Application.persistentDataPath;
        var saveFile = Path.Combine(baseSavePath, this.name);
        var savedDatas = FileReadWrite.ReadFromBinaryFile<List<InventorySaveData>>(saveFile + ".dat");
        inventorySlots.Clear();
        for (int i = 0; i < savedDatas.Count; i++)
        {
            AddItem(new Item(ItemDatabase.GetItemSOReferenceById(savedDatas[i].ItemID), savedDatas[i].Buffs), savedDatas[i].Amount);
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
}

/// <summary>
/// Class slot item on inventory that contains itemID, itemSO and amount.
/// </summary>
[System.Serializable]
public class InventorySlot
{
    public int ItemID;
    public Item Item;
    public int Amount;

    /// <summary>
    /// Constuctor InventorySlot.
    /// </summary>
    public InventorySlot()
    {
        ItemID = -1;
        Item = null;
        Amount = 0;
    }

    /// <summary>
    /// Constructor InventorySlot with ItemSO and Amount.
    /// </summary>
    /// <param name="_itemSO">itemSO</param>
    /// <param name="_amount">item amount</param>
    public InventorySlot(Item _itemSO, int _amount)
    {
        ItemID = _itemSO.Id;
        Item = _itemSO;
        Amount = _amount;
    }
}

/// <summary>
/// Class that represent save data format.
/// </summary>
[System.Serializable]
public class InventorySaveData
{
    /// <summary>
    /// Id of the item that will be to save.
    /// </summary>
    public int ItemID;
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
    /// <param name="_itemId">Id of the item that we want to save.</param>
    /// <param name="_amount">Amount of the item that we want to save.</param>
    /// <param name="_buffs">Item buffs that we want to save.</param>
    public InventorySaveData(int _itemId, int _amount, ItemBuff[] _buffs)
    {
        ItemID = _itemId;
        Amount = _amount;
        Buffs = _buffs;
    }
}

/// <summary>
/// Comparer class list by item type.
/// </summary>
class SlotTypeComparer : IComparer<InventorySlot>
{
    /// <summary>
    /// Database that hold all item and funtionality get itemSO using item id.
    /// </summary>
    ItemDatabaseSO databaseSO;

    /// <summary>
    /// Comparer to sort list by item type.
    /// </summary>
    /// <param name="_databaseSO">Database.</param>
    public SlotTypeComparer(ItemDatabaseSO _databaseSO)
    {
        databaseSO = _databaseSO;
    }
    public int Compare(InventorySlot x, InventorySlot y)
    {
        return databaseSO.GetItemSOReferenceById(x.ItemID).ItemType.CompareTo(
            databaseSO.GetItemSOReferenceById(y.ItemID).ItemType
        );
    }
}

/// <summary>
/// Comparer class list by item name.
/// </summary>
class SlotNameComparer : IComparer<InventorySlot>
{
    /// <summary>
    /// Database that hold all item and funtionality get itemSO using item id.
    /// </summary>
    private ItemDatabaseSO databaseSO;

    /// <summary>
    /// Comparer to sort list by item name.
    /// </summary>
    /// <param name="_databaseSO">Database.</param>
    public SlotNameComparer(ItemDatabaseSO _databaseSO)
    {
        databaseSO = _databaseSO;
    }
    public int Compare(InventorySlot x, InventorySlot y)
    {
        return databaseSO.GetItemSOReferenceById(x.ItemID).ItemName.CompareTo(
            databaseSO.GetItemSOReferenceById(y.ItemID).ItemName
        );
    }
}