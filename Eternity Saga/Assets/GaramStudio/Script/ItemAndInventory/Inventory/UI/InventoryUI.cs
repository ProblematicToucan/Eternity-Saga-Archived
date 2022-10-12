using UnityEngine;

public abstract class InventoryUI : MonoBehaviour, IRecyclerviewDelegate
{
    [field: SerializeField] public Recyclerview Recyclerview { get; private set; }
    [field: SerializeField] public RecyclerCellView RecyclerCellViewPrefab { get; private set; }
    [Header("Events")]
    [SerializeField] private EventSO[] onEnableEvents; // Array event to be triggered when the inventory is enabled.
    [SerializeField] private EventSO[] onDisableEvents; // Array event to be triggered when the inventory is disabled.

    public virtual void OnEnable()
    {
        CellViewSelected(null);
        for (var i = 0; i < onEnableEvents.Length; i++)
        {
            if (onEnableEvents[i] != null)
                onEnableEvents[i].Raise();
        }
    }

    public virtual void OnDisable()
    {
        for (var i = 0; i < onDisableEvents.Length; i++)
        {
            onDisableEvents[i].Raise();
        }
    }

    public virtual void Start()
    {
        Recyclerview.Delegate = this;
        Recyclerview.ReloadData();
    }

    public abstract void RefreshDisplay();

    public abstract int GetNumberOfCells(Recyclerview recyclerview);

    public abstract float GetCellViewSize(Recyclerview recyclerview, int dataIndex);

    public abstract RecyclerCellView GetCellView(Recyclerview recyclerview, int dataIndex, int cellIndex);

    public abstract void CellViewSelected(RecyclerCellView cellView);
}