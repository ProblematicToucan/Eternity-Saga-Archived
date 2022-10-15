using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagUI : InventoryUI
{
    [Header("Bag Property")]
    [SerializeField] private InventorySO inventorySO; // Inventory ScriptableObject that hold data to show.
    [SerializeField] private GameObject itemDetailsPanel;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Button lockButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button enhanceButton;
    [SerializeField] private Button removeButton;
    private InventorySlot selectedInventorySlot;

    public override void OnEnable()
    {
        base.OnEnable();
        RefreshDisplay();
        inventorySO.OnInventoryChanged += RefreshDisplay;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        selectedInventorySlot = null;
        inventorySO.OnInventoryChanged -= RefreshDisplay;
    }

    public override void RefreshDisplay()
    {
        Recyclerview.ReloadData();
        RefreshDetails();
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
            itemSO.ItemIcon,
            itemSO.ItemName,
            slots[dataIndex].Amount
        );
        return cellView;
    }

    public override void CellViewSelected(RecyclerCellView cellView)
    {
        if (cellView == null)
        {
            itemDetailsPanel.SetActive(false);
            selectedInventorySlot = null;
        }
        else
        {
            itemDetailsPanel.SetActive(false);
            var selectedDataIndex = (cellView as ItemSlotUI).DataIndex;
            selectedInventorySlot = inventorySO.inventorySlots[selectedDataIndex];
            var itemSO = inventorySO.ItemDatabase.GetItemSOReferenceById(selectedInventorySlot.ItemId);
            string[] itemNameText = { $"{selectedInventorySlot.Item.Name}",
                $"({itemSO.ItemType})",
                $"({selectedInventorySlot.Amount})"};
            equipButton.interactable = itemSO.ItemType != ItemType.Misc;
            enhanceButton.interactable = itemSO.ItemType == ItemType.Equipment;
            itemDetailsPanel.SetActive(true);
            itemImage.sprite = itemSO.ItemIcon;
            itemName.text = string.Join(" ", itemNameText);
            itemDescription.text = itemSO.ItemDescription;
        }
    }

    private void RefreshDetails()
    {
        itemDetailsPanel.SetActive(false);
        selectedInventorySlot = inventorySO.inventorySlots.Contains(selectedInventorySlot) ?
            selectedInventorySlot : null;
        if (selectedInventorySlot != null)
        {
            var itemSO = inventorySO.ItemDatabase.GetItemSOReferenceById(selectedInventorySlot.ItemId);
            string[] itemNameText = { $"{selectedInventorySlot.Item.Name}",
                $"({itemSO.ItemType})",
                $"({selectedInventorySlot.Amount})"};
            itemDetailsPanel.SetActive(true);
            itemName.text = string.Join(" ", itemNameText);
        }
    }

    public void RemoveItem()
    {
        inventorySO.RemoveItem(selectedInventorySlot, 1);
    }
}