using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventorySO inventory;
    [SerializeField] private UIInventorySlot[] UiInventorySlot;

    private void Start()
    {
        inventory.OnInventoryChanged += RefreshDisplay;

        for (int i = 0; i < inventory.GetInventorySlots().Count; i++)
        {
            var itemSlot = inventory.GetInventorySlots()[i];
            var itemSO = inventory.ItemDatabaseSO().GetItemSO(itemSlot.ItemID);
            UiInventorySlot[i].SetSlot(itemSlot.ItemID, itemSO.ItemIcon, itemSlot.ItemCount);
        }
    }

    private void OnDestroy()
    {
        inventory.OnInventoryChanged -= RefreshDisplay;
    }

    private void RefreshDisplay()
    {

    }
}
