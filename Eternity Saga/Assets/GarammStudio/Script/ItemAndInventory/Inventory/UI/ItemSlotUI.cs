using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

/// <summary>
/// Delegate untuk menghandle UI button komponen.
/// </summary>
/// <param name="cellView">CellView yang memiliki komponen button klik.</param>
public delegate void SelectedDelegate(RecyclerCellView cellView);

/// <summary>
/// Refference class to the item slot UI.
/// </summary>
public class ItemSlotUI : RecyclerCellView
{
    public ItemSO Item;
    [field: SerializeField] public Image ItemImage { get; private set; }
    [field: SerializeField] public Image BackgroundImage { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ItemCount { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ItemName { get; private set; }
    [field: SerializeField] public Color selectedColor { get; private set; }
    [field: SerializeField] public Color UnSelectedColor { get; private set; }
    /// <summary>
    /// Public reference index from some data.
    /// </summary>
    public int DataIndex { get; private set; }
    /// <summary>
    /// The handler to call when this cell's button traps a click event.
    /// </summary>
    public SelectedDelegate selected;

    private void SelectedChanged(bool selected)
    {
        BackgroundImage.color = (selected ? selectedColor : UnSelectedColor);
    }

    public async void SetData(ItemSO _item, int _amount)
    {
        Item = _item;
        ItemImage.sprite = _item.ItemIcon;
        ItemCount.text = _amount.ToString();
        ItemName.text = _item.ItemName;
        await Task.Yield();
    }
}