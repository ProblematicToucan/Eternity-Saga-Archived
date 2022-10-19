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
        RefreshDisplay();
        for (var i = 0; i < onEnableEvents.Length; i++)
        {
            if (onEnableEvents[i] != null)
                onEnableEvents[i].Raise();
        }
    }

    public virtual void OnDisable()
    {
        CellViewSelected(null);
        for (var i = 0; i < onDisableEvents.Length; i++)
        {
            onDisableEvents[i].Raise();
        }
    }

    public virtual void Start()
    {
        Recyclerview.Delegate = this;
        RefreshDisplay();
    }

    /// <summary>
    /// Mehod for refreshing display.
    /// </summary>
    public abstract void RefreshDisplay();

    /// <summary>
    /// This callback tells the Recyclerview how many inventory items to expect.
    /// </summary>
    /// <param name="recyclerview">The Recyclerview requesting the number of cells.</param>
    /// <returns>The number of cells.</returns>
    public abstract int GetNumberOfCells(Recyclerview recyclerview);

    /// <summary>
    /// This callback tells the Recyclerview what size each cell is.
    /// </summary>
    /// <param name="recyclerview">The Recyclerview requesting the cell size.</param>
    /// <param name="dataIndex">The index of the data list.</param>
    /// <returns>The size of the cell (Height for vertical scrollers, Width for Horizontal scrollers).</returns>
    public abstract float GetCellViewSize(Recyclerview recyclerview, int dataIndex);

    /// <summary>
    /// This callback gets the cell to be displayed by the Recyclerview.
    /// </summary>
    /// <param name="recyclerview">The Recyclerview requesting the cell.</param>
    /// <param name="dataIndex">The index of the data list.</param>
    /// <param name="cellIndex">The cell index (This will be different from dataindex if looping is involved).</param>
    /// <returns>The cell to display.</returns>
    public abstract RecyclerCellView GetCellView(Recyclerview recyclerview, int dataIndex, int cellIndex);


    /// <summary>
    /// This function handles the cell view's button click event.
    /// </summary>
    /// <param name="cellView">The cell view that had the button clicked.</param>
    public abstract void CellViewSelected(RecyclerCellView cellView);
}