using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

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
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].Item != null)
            {
                inventorySlots[i].ItemID = inventorySlots[i].Item.Id;
            }
        }
    }
#endif

    public void AddItem(Item _item, int _amount)
    {
        var hasItem = false;
        var itemOnDatabase = ItemDatabase.GetItemSOReferenceById(_item.Id);
        if (itemOnDatabase == null) return;
        if (_item.Buffs.Length > 0)
        {
            inventorySlots.Add(new InventorySlot(_item, _amount));
            OnInventoryChanged?.Invoke();
            return;
        }
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].Item.Id == _item.Id)
            {
                inventorySlots[i].Amount += _amount;
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
        ItemDrop.OnTouch += OnTouch;
    }

    public void UnregisterEvent()
    {
        ItemDrop.OnTouch -= OnTouch;
        inventorySlots.Clear();
    }

    private void OnTouch(Item obj, int amount)
    {
        AddItem(obj, amount);
    }

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

[System.Serializable]
public class InventorySaveData
{
    public int ItemID;
    public int Amount;
    public ItemBuff[] Buffs;
    public InventorySaveData(int _itemId, int _amount, ItemBuff[] _buffs)
    {
        ItemID = _itemId;
        Amount = _amount;
        Buffs = _buffs;
    }
}