using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<ItemSO> items;
    [SerializeField] private ItemSlotUI[] itemSlots;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private EventSO onEnableEvent;
    [SerializeField] private EventSO onDisableEvent;

    private void OnEnable()
    {
        onEnableEvent?.Raise();
    }

    private void OnDisable()
    {
        onDisableEvent?.Raise();
    }
}
