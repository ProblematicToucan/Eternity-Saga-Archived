using UnityEngine;

public class InventoryUI : MonoBehaviour, IRecyclerviewDelegate
{
    [field: SerializeField] public Recyclerview Recyclerview { get; private set; }
    [field: SerializeField] public RecyclerCellView RecyclerCellViewPrefab { get; private set; }
    [SerializeField] private InventorySO inventorySO; // Inventory ScriptableObject that hold data to show.
    [SerializeField] private EventSO[] onEnableEvents; // Array event to be triggered when the inventory is enabled.
    [SerializeField] private EventSO[] onDisableEvents; // Array event to be triggered when the inventory is disabled.
    private void OnEnable()
    {
        for (var i = 0; i < onEnableEvents.Length; i++)
        {
            onEnableEvents[i]?.Raise();
        }
        inventorySO.OnInventoryChanged += RefreshDisplay;
        RefreshDisplay();
    }

    private void OnDisable()
    {
        for (var i = 0; i < onDisableEvents.Length; i++)
        {
            onDisableEvents[i]?.Raise();
        }
        inventorySO.OnInventoryChanged -= RefreshDisplay;
    }

    private void Start()
    {
        Recyclerview.Delegate = this;
        Recyclerview.ReloadData();
    }

    public void RefreshDisplay()
    {
        Recyclerview.ReloadData();
    }

    public int GetNumberOfCells(Recyclerview recyclerview)
    {
        return inventorySO.inventorySlots.Count;
    }

    public float GetCellViewSize(Recyclerview recyclerview, int dataIndex)
    {
        return 200;
    }

    public RecyclerCellView GetCellView(Recyclerview recyclerview, int dataIndex, int cellIndex)
    {
        var cellView = Recyclerview.GetCellView(RecyclerCellViewPrefab) as ItemSlotUI;
        cellView!.name = dataIndex.ToString();
        cellView.SetData(
            inventorySO.ItemDatabase.GetItemSOReferenceById(inventorySO.inventorySlots[dataIndex].ItemId),
            inventorySO.inventorySlots[dataIndex].Amount
        );
        return cellView;
    }
}