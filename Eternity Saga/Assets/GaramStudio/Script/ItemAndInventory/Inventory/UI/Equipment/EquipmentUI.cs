using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : InventoryUI
{
    [Header("Equipment Property")]
    [SerializeField] private InventorySO equipmentInventorySO;
    [SerializeField] private EquipmentBagUI equipmentBagUI;
    [SerializeField] private GameObject itemDetailsPanel;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button unequipButton;
    [SerializeField] private Button enhanceButton;
    private InventorySlot selectedEquipedSlot;
    public override void OnEnable()
    {
        base.OnEnable();

        equipmentInventorySO.OnInventoryChanged += RefreshDisplay;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        equipmentInventorySO.OnInventoryChanged -= RefreshDisplay;
    }

    public override void RefreshDisplay()
    {
        // tell the scrollers to reload.
        Recyclerview.ReloadData();

        // tell details panel to reload.
        RefreshDetails();
    }

    public override int GetNumberOfCells(Recyclerview recyclerview)
    {
        return equipmentInventorySO.inventorySlots.Count;
    }

    public override float GetCellViewSize(Recyclerview recyclerview, int dataIndex)
    {
        return 200;
    }

    public override RecyclerCellView GetCellView(Recyclerview recyclerview, int dataIndex, int cellIndex)
    {
        var cellView = Recyclerview.GetCellView(RecyclerCellViewPrefab) as ItemSlotUI;
        var slots = equipmentInventorySO.inventorySlots;
        var itemSO = equipmentInventorySO.ItemDatabase.GetItemSOReferenceById(slots[dataIndex].ItemId);
        cellView.name = dataIndex.ToString();

        // set the selected callback to the CellViewSelected function of this controller. 
        // this will be fired when the cell's button is clicked.
        cellView.selected = CellViewSelected;

        // set the data for the cell.
        cellView.SetData(
            dataIndex,
            slots[dataIndex],
            itemSO != null ? itemSO.ItemIcon : null
        );
        return cellView;
    }

    public override void CellViewSelected(RecyclerCellView cellView)
    {
        if (cellView == null)
        {
            itemDetailsPanel.SetActive(false);
            equipmentBagUI.RecyclerviewSetActive = false;
            selectedEquipedSlot = null;
            for (int i = 0; i < equipmentInventorySO.inventorySlots.Count; i++)
            {
                equipmentInventorySO.inventorySlots[i].Selected = false;
            }
        }
        else
        {
            itemDetailsPanel.SetActive(false);
            equipmentBagUI.RecyclerviewSetActive = false;
            var selectedDataIndex = (cellView as ItemSlotUI).DataIndex;
            selectedEquipedSlot = equipmentInventorySO.inventorySlots[selectedDataIndex];
            for (int i = 0; i < equipmentInventorySO.inventorySlots.Count; i++)
                equipmentInventorySO.inventorySlots[i].Selected = (selectedDataIndex == i);

            var itemSO = equipmentInventorySO.ItemDatabase.GetItemSOReferenceById(selectedEquipedSlot.ItemId) as EquippableItemSO;
            string[] itemNameText =
                selectedEquipedSlot.ItemId != -1 ?
                new[] { $"{selectedEquipedSlot.Item.Name}",
                $"({itemSO.ItemType})",
                $"({selectedEquipedSlot.Amount})"} :
                new[] { $"No {(cellView as ItemSlotUI).ItemSlotNameCase(selectedDataIndex)}" };

            equipButton.interactable = false;
            equipmentBagUI.RecyclerviewSetActive = true;
            unequipButton.interactable = selectedEquipedSlot.ItemId != -1;
            enhanceButton.interactable = selectedEquipedSlot.ItemId != -1;
            itemDetailsPanel.SetActive(true);

            itemImage.sprite = selectedEquipedSlot.ItemId != -1 ? itemSO.ItemIcon : (cellView as ItemSlotUI).ItemSlotImageCase(selectedDataIndex);
            itemName.text = string.Join(" ", itemNameText);
            itemDescription.text = selectedEquipedSlot.ItemId != -1 ? itemSO.ItemDescription : null;
        }
    }

    /// <summary>
    /// Mehod to refresh details panel.
    /// </summary>
    private void RefreshDetails()
    {
        selectedEquipedSlot = equipmentInventorySO.inventorySlots.Contains(selectedEquipedSlot) ?
            selectedEquipedSlot : null;
        itemDetailsPanel.SetActive(selectedEquipedSlot != null);
    }

    public void EquipItem(InventorySlot inventorySlot)
    {
        var index = equipmentInventorySO.inventorySlots.IndexOf(selectedEquipedSlot);
        if (selectedEquipedSlot.ItemId != -1) UnequipItem();
        equipmentBagUI.RecyclerviewSetActive = false;
        equipmentInventorySO.inventorySlots[index] = inventorySlot;
        CellViewSelected(null);
        RefreshDisplay();
    }

    public void UnequipItem()
    {
        var index = equipmentInventorySO.inventorySlots.IndexOf(selectedEquipedSlot);
        selectedEquipedSlot.Selected = false;
        equipmentInventorySO.inventorySlots[index] = new InventorySlot();
        equipmentBagUI.UnequipItem(selectedEquipedSlot);
        equipmentBagUI.RecyclerviewSetActive = false;
        RefreshDisplay();
    }
}