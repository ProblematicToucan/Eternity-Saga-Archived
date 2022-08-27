using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "new Inventory", menuName = "GarammStudio/Items Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    public event UnityAction OnInventoryChanged;
    [SerializeField] private ItemDatabaseSO itemDatabase;
    [field: SerializeField] public int capacity { get; private set; } = 50;
    public List<InventorySlot> inventorySlots;
    private const string InventoryFileName = "Inventory";

    public bool IsFull() => inventorySlots.Count >= capacity;

#if UNITY_EDITOR
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
#endif

    public void AddItem(ItemSO _item, int _amount)
    {
        var hasItem = false;
        var itemIdOnDatabase = itemDatabase.GetIdReferenceByItemSO(_item);
        var itemOnDatabase = itemDatabase.GetItemSOReferenceById(itemIdOnDatabase);
        if (itemOnDatabase == null) return;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item.itemID == itemIdOnDatabase)
            {
                inventorySlots[i].amount += _amount;
                hasItem = true;
                break;
            }
        }
        if (!hasItem && !IsFull())
        {
            inventorySlots.Add(new InventorySlot(itemOnDatabase, _amount));
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

    [ContextMenu("Save")]
    public void Save()
    {
        var saveDatas = new List<InventorySaveData>();
        var baseSavePath = Application.persistentDataPath;
        var saveFile = Path.Combine(baseSavePath, InventoryFileName);
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            saveDatas.Add(new InventorySaveData(inventorySlots[i].itemID, inventorySlots[i].amount));
        }
        FileReadWrite.WriteToBinaryFile(saveFile + ".dat", saveDatas);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        var baseSavePath = Application.persistentDataPath;
        var saveFile = Path.Combine(baseSavePath, InventoryFileName);
        var savedDatas = FileReadWrite.ReadFromBinaryFile<List<InventorySaveData>>(saveFile + ".dat");
        inventorySlots.Clear();
        for (int i = 0; i < savedDatas.Count; i++)
        {
            AddItem(itemDatabase.GetItemSOReferenceById(savedDatas[i].itemID), savedDatas[i].amount);
        }
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
    public InventorySlot(ItemSO _itemSO, int _amount)
    {
        itemID = _itemSO.itemID;
        item = _itemSO;
        amount = _amount;
    }
}

[System.Serializable]
public class InventorySaveData
{
    public int itemID;
    public int amount;
    public InventorySaveData(int _itemId, int _amount)
    {
        itemID = _itemId;
        amount = _amount;
    }
}