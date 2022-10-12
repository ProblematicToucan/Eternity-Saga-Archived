using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentBagUI : InventoryUI
{
    [SerializeField] protected InventorySO inventorySO;
    [Header("Item Details Property")]
    [SerializeField] private GameObject ItemDetailsPanel;
    [SerializeField] private Image ItemImage;
    [SerializeField] private TextMeshProUGUI ItemName;
    [SerializeField] private TextMeshProUGUI ItemDescription;
    private List<InventorySlot> GetEquipmentOnInventorySlots
    {
        get
        {
            var slotNameComparer = new SlotNameComparer(inventorySO.ItemDatabase);
            List<InventorySlot> equipmentSlots = new List<InventorySlot>();
            for (var i = 0; i < inventorySO.inventorySlots.Count; i++)
            {
                var itemType = inventorySO.ItemDatabase.GetItemSOReferenceById(inventorySO.inventorySlots[i].ItemId).ItemType;
                if (itemType == ItemType.Equipment)
                {
                    equipmentSlots.Add(inventorySO.inventorySlots[i]);
                    equipmentSlots.Sort(slotNameComparer);
                }
            }
            return equipmentSlots;
        }
    }
    private int GetEquipmentCount
    {
        get
        {
            var count = 0;
            for (var i = 0; i < inventorySO.inventorySlots.Count; i++)
            {
                if (inventorySO.ItemDatabase.GetItemSOReferenceById(inventorySO.inventorySlots[i].ItemId).ItemType == ItemType.Equipment)
                {
                    count++;
                }
            }
            return count;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        RefreshDisplay();
        inventorySO.OnInventoryChanged += RefreshDisplay;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        inventorySO.OnInventoryChanged -= RefreshDisplay;
    }

    public override void RefreshDisplay()
    {
        Recyclerview.ReloadData();
    }

    public override int GetNumberOfCells(Recyclerview recyclerview)
    {
        return GetEquipmentCount;
    }

    public override float GetCellViewSize(Recyclerview recyclerview, int dataIndex)
    {
        return 200;
    }

    public override RecyclerCellView GetCellView(Recyclerview recyclerview, int dataIndex, int cellIndex)
    {
        var cellView = Recyclerview.GetCellView(RecyclerCellViewPrefab) as ItemSlotUI;
        var slots = GetEquipmentOnInventorySlots;
        var itemSO = inventorySO.ItemDatabase.GetItemSOReferenceById(slots[dataIndex].ItemId);
        cellView.name = dataIndex.ToString();
        cellView.selected = CellViewSelected;
        cellView.SetData(
            dataIndex,
            itemSO,
            slots[dataIndex].Amount
        );
        return cellView;
    }

    public override void CellViewSelected(RecyclerCellView cellView)
    {
        if (cellView == null)
        {
            ItemDetailsPanel.SetActive(false);
        }
        else
        {
            ItemDetailsPanel.SetActive(false);
            var selectedDataIndex = (cellView as ItemSlotUI).DataIndex;
            var slots = GetEquipmentOnInventorySlots[selectedDataIndex];
            var itemSO = inventorySO.ItemDatabase.GetItemSOReferenceById(slots.ItemId);
            string[] itemNameText = { $"{slots.Item.Name}",
                $"({itemSO.ItemType})",
                $"({slots.Amount})"};
            ItemDetailsPanel.SetActive(true);
            ItemImage.sprite = itemSO.ItemIcon;
            ItemName.text = string.Join(" ", itemNameText);
            ItemDescription.text = itemSO.ItemDescription;
        }
    }
}