using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public ItemSO Item { get; private set; }
    [field: SerializeField] public Image ItemImage { get; private set; }
    [field: SerializeField] public Image BackgroundImage { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ItemCount { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ItemName { get; private set; }
    [field: SerializeField] public Color selectedColor { get; private set; }
    [field: SerializeField] public Color UnSelectedColor { get; private set; }

    /// <summary>
    /// Index properti publik dari data pada cell view ini.
    /// </summary>
    public int DataIndex { get; private set; }

    /// <summary>
    /// Handler yang dipanggil ketika cell view ini di trigger oleh event klik.
    /// </summary>
    public SelectedDelegate selected;

    private void SelectedChanged(bool selected)
    {
        BackgroundImage.color = (selected ? selectedColor : UnSelectedColor);
    }

    public async void SetData(int dataIndex, ItemSO _item, int _amount)
    {
        DataIndex = dataIndex;
        Item = _item;
        ItemImage.sprite = _item.ItemIcon;
        ItemCount.text = _amount.ToString();
        ItemName.text = _item.ItemName;
        await Task.Yield();
    }

    public void OnSelected()
    {
        selected?.Invoke(this);
    }
}