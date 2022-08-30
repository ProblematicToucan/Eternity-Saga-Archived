using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class InventoryUI : MonoBehaviour
{
    private Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    [SerializeField] private InventorySO inventorySO;
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
        inventorySO.OnInventoryChanged += RefreshDisplay;
        RefreshDisplay();
    }

    private void OnDisable()
    {
        for (int i = 0; i < onDisableEvents.Length; i++)
        {
            onDisableEvents[i]?.Raise();
        }
        inventorySO.OnInventoryChanged -= RefreshDisplay;
    }

    public async void RefreshDisplay()
    {
        for (int i = 0; i < inventorySO.inventorySlots.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventorySO.inventorySlots[i]))
            {
                var itemSO = inventorySO.ItemDatabase.GetItemSOReferenceById(inventorySO.inventorySlots[i].Item.Id);
                itemsDisplayed[inventorySO.inventorySlots[i]]
                    .GetComponent<ItemSlotUI>()
                    .UpdateDisplay(itemSO, inventorySO.inventorySlots[i].Amount);
            }
            else
            {
                var itemSO = inventorySO.ItemDatabase.GetItemSOReferenceById(inventorySO.inventorySlots[i].Item.Id);
                var clonedItemSlot = Instantiate(itemSlotPrefab, inventoryPanel);
                var itemSlotUI = clonedItemSlot.GetComponent<ItemSlotUI>();
                itemSlotUI.UpdateDisplay(itemSO, inventorySO.inventorySlots[i].Amount);
                itemsDisplayed.Add(inventorySO.inventorySlots[i], clonedItemSlot);
            }
        }
        var arrayOfAllKeys = itemsDisplayed.Keys.ToArray();
        for (int i = arrayOfAllKeys.Length - 1; i >= 0; i--)
        {
            if (!inventorySO.inventorySlots.Contains(arrayOfAllKeys[i]))
            {
                Destroy(itemsDisplayed[arrayOfAllKeys[i]]);
                itemsDisplayed.Remove(arrayOfAllKeys[i]);
            }
        }
        await Task.Yield();
    }
}