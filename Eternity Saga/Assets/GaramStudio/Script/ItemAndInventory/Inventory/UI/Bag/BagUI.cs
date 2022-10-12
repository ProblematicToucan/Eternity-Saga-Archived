using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagUI : InventoryUI
{
    [SerializeField] protected InventorySO inventorySO; // Inventory ScriptableObject that hold data to show.
    [Header("Item Details Property")]
    [SerializeField] private GameObject ItemDetailsPanel;
    [SerializeField] private Image ItemImage;
    [SerializeField] private TextMeshProUGUI ItemName;
    [SerializeField] private TextMeshProUGUI ItemDescription;

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
        return inventorySO.inventorySlots.Count;
    }

    public override float GetCellViewSize(Recyclerview recyclerview, int dataIndex)
    {
        return 200;
    }

    public override RecyclerCellView GetCellView(Recyclerview recyclerview, int dataIndex, int cellIndex)
    {
        var cellView = Recyclerview.GetCellView(RecyclerCellViewPrefab) as ItemSlotUI;
        var slots = inventorySO.inventorySlots;
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
            var inventorySlot = inventorySO.inventorySlots[selectedDataIndex];
            var itemSO = inventorySO.ItemDatabase.GetItemSOReferenceById(inventorySlot.ItemId);
            string[] itemNameText = { $"{inventorySlot.Item.Name}",
                $"({itemSO.ItemType})",
                $"({inventorySlot.Amount})"};
            ItemDetailsPanel.SetActive(true);
            ItemImage.sprite = itemSO.ItemIcon;
            ItemName.text = string.Join(" ", itemNameText);
            ItemDescription.text = itemSO.ItemDescription;
        }
    }
}
