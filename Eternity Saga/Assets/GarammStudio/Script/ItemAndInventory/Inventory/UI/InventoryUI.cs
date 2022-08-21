using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    [SerializeField] private InventorySO inventory;
    [SerializeField] private GameObject itemSlotPrefab; // Item slot prefab to be instantiate.
    [SerializeField] private RectTransform inventoryPanel; // Inventory Panel for parenting all the item slots
    [SerializeField] private EventSO[] onEnableEvents; // Array event to be triggered when the inventory is enabled.
    [SerializeField] private EventSO[] onDisableEvents; // Array event to be triggered when the inventory is disabled.

    private void OnEnable()
    {
        for (int i = 0; i < onEnableEvents.Length; i++)
        {
            onEnableEvents[i]?.Raise();
        }
        inventory.OnInventoryChanged += RefreshDisplay;
        RefreshDisplay();
    }

    private void OnDisable()
    {
        for (int i = 0; i < onDisableEvents.Length; i++)
        {
            onDisableEvents[i]?.Raise();
        }
        inventory.OnInventoryChanged -= RefreshDisplay;
    }

    public void RefreshDisplay()
    {
        for (int i = 0; i < inventory.inventorySlots.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.inventorySlots[i]))
            {
                itemsDisplayed[inventory.inventorySlots[i]]
                    .GetComponent<ItemSlotUI>()
                    .UpdateDisplay(inventory.inventorySlots[i].item, inventory.inventorySlots[i].amount);
            }
            else
            {
                var clonedItemSlot = Instantiate(itemSlotPrefab, inventoryPanel);
                var itemSlotUI = clonedItemSlot.GetComponent<ItemSlotUI>();
                itemSlotUI.UpdateDisplay(inventory.inventorySlots[i].item, inventory.inventorySlots[i].amount);
                itemsDisplayed.Add(inventory.inventorySlots[i], clonedItemSlot);
            }
        }
    }
}