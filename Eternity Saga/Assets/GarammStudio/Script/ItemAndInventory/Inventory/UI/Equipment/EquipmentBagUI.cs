using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentBagUI : InventoryUI
{
    [Header("Equipment Bag Property")]
    [SerializeField] private InventorySO bagInventorySO;
    [SerializeField] private EquipmentUI equipmentUI;
    [SerializeField] private GameObject itemDetailsPanel;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button unequipButton;
    [SerializeField] private Button enhanceButton;
    private bool _recyclerviewSetActive;
    private InventorySlot selectedInventorySlot;
    public bool RecyclerviewSetActive
    {
        get { return _recyclerviewSetActive; }
        set
        {
            _recyclerviewSetActive = value;

            // if the data existed previously, loop through
            // and remove the selection change handlers before
            // clearing out the data.
            for (var i = 0; i < bagInventorySO.inventorySlots.Count; i++)
            {
                bagInventorySO.inventorySlots[i].selectedChanged = null;
            }

            RefreshDisplay();
        }
    }
    private List<InventorySlot> GetEquipmentOnInventorySlots
    {
        get
        {
            var slotNameComparer = new SlotNameComparer(bagInventorySO.ItemDatabase);
            List<InventorySlot> equipmentSlots = new List<InventorySlot>();
            for (var i = 0; i < bagInventorySO.inventorySlots.Count; i++)
            {
                var itemType = bagInventorySO.ItemDatabase.GetItemSOReferenceById(bagInventorySO.inventorySlots[i].ItemId).ItemType;
                if (itemType == ItemType.Equipment)
                {
                    equipmentSlots.Add(bagInventorySO.inventorySlots[i]);
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
            for (var i = 0; i < bagInventorySO.inventorySlots.Count; i++)
            {
                if (bagInventorySO.ItemDatabase.GetItemSOReferenceById(bagInventorySO.inventorySlots[i].ItemId).ItemType == ItemType.Equipment)
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

        bagInventorySO.OnInventoryChanged += RefreshDisplay;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        bagInventorySO.OnInventoryChanged -= RefreshDisplay;
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
        return RecyclerviewSetActive ? GetEquipmentCount : 0;
    }

    public override float GetCellViewSize(Recyclerview recyclerview, int dataIndex)
    {
        return 200;
    }

    public override RecyclerCellView GetCellView(Recyclerview recyclerview, int dataIndex, int cellIndex)
    {
        var cellView = Recyclerview.GetCellView(RecyclerCellViewPrefab) as ItemSlotUI;
        var slots = GetEquipmentOnInventorySlots;
        var itemSO = bagInventorySO.ItemDatabase.GetItemSOReferenceById(slots[dataIndex].ItemId);
        cellView.name = dataIndex.ToString();

        // set the selected callback to the CellViewSelected function of this controller. 
        // this will be fired when the cell's button is clicked.
        cellView.selected = CellViewSelected;

        // set the data for the cell.
        cellView.SetData(
            dataIndex,
            slots[dataIndex],
            itemSO.ItemIcon
        );
        return cellView;
    }

    public override void CellViewSelected(RecyclerCellView cellView)
    {
        if (cellView == null)
        {
            itemDetailsPanel.SetActive(false);
            selectedInventorySlot = null;
            for (int i = 0; i < bagInventorySO.inventorySlots.Count; i++)
            {
                bagInventorySO.inventorySlots[i].Selected = false;
            }
        }
        else
        {
            itemDetailsPanel.SetActive(false);
            var selectedDataIndex = (cellView as ItemSlotUI).DataIndex;
            selectedInventorySlot = GetEquipmentOnInventorySlots[selectedDataIndex];
            for (int i = 0; i < GetEquipmentOnInventorySlots.Count; i++)
                GetEquipmentOnInventorySlots[i].Selected = (selectedDataIndex == i);

            var itemSO = bagInventorySO.ItemDatabase.GetItemSOReferenceById(selectedInventorySlot.ItemId) as EquippableItemSO;
            string[] itemNameText = { $"{selectedInventorySlot.Item.Name}",
                $"({itemSO.ItemType})",
                $"({selectedInventorySlot.Amount})"};

            unequipButton.interactable = false;
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
        selectedInventorySlot = bagInventorySO.inventorySlots.Contains(selectedInventorySlot) ?
            selectedInventorySlot : null;
        if (selectedInventorySlot != null)
        {
            var itemSO = bagInventorySO.ItemDatabase.GetItemSOReferenceById(selectedInventorySlot.ItemId);
            string[] itemNameText = { $"{selectedInventorySlot.Item.Name}",
                $"({itemSO.ItemType})",
                $"({selectedInventorySlot.Amount})"};
            itemName.text = string.Join(" ", itemNameText);
            itemDetailsPanel.SetActive(true);
        }
    }

    public void EquipItem()
    {
        var slots = bagInventorySO.inventorySlots;
        equipmentUI.EquipItem(selectedInventorySlot);
        slots.Remove(selectedInventorySlot);
        CellViewSelected(null);
    }

    public void UnequipItem(InventorySlot inventorySlot)
    {
        var slots = bagInventorySO.inventorySlots;
        slots.Add(inventorySlot);
        bagInventorySO.SortSlot();
        RefreshDisplay();
    }
}