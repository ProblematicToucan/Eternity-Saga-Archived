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
    [field: SerializeField] public Image ItemImage { get; private set; }
    [SerializeField] private Image maskImage;
    [field: SerializeField] public Image BackgroundImage { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ItemCount { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ItemName { get; private set; }
    [field: SerializeField] public Color SelectedColor { get; private set; }
    [field: SerializeField] public Color UnSelectedColor { get; private set; }
    [SerializeField] private Color primaryColor;
    [SerializeField] private Color secondaryColor;

    [Header("Default slot images")]
    [SerializeField] private Sprite weaponImage;
    [SerializeField] private Sprite helmetImage;
    [SerializeField] private Sprite upperImage;
    [SerializeField] private Sprite bottomImage;
    [SerializeField] private Sprite accesoryImage;
    private InventorySlot _data;

    /// <summary>
    /// Index properti publik dari data pada cell view ini.
    /// </summary>
    public int DataIndex { get; private set; }

    /// <summary>
    /// Handler yang dipanggil ketika cell view ini di trigger oleh event klik.
    /// </summary>
    public SelectedDelegate selected;

    private void OnDestroy()
    {
        if (_data != null) _data.selectedChanged -= SelectedChanged;
    }

    private void SelectedChanged(bool selected)
    {
        BackgroundImage.color = (selected ? SelectedColor : UnSelectedColor);
    }

    public void SetData(int dataIndex, InventorySlot data, Sprite itemImage)
    {
        // if there was previous data assigned to this cell view,
        // we need to remove the handler for the selection change
        if (_data != null) _data.selectedChanged -= SelectedChanged;
        _data = data;

        maskImage.color = data.ItemId != -1 ? primaryColor : secondaryColor;
        DataIndex = dataIndex;
        ItemImage.sprite = itemImage != null ? itemImage : ItemSlotImageCase(dataIndex);
        ItemName.text = data.ItemId != -1 ? data.Item.Name : ItemSlotNameCase(dataIndex);
        ItemCount.text = data.Amount == 0 ? null : data.Amount.ToString();

        // set up a handler so that when the data changes
        // the cell view will update accordingly. We only
        // want a single handler for this cell view, so 
        // first we remove any previous handlers before
        // adding the new one
        _data.selectedChanged -= SelectedChanged;
        _data.selectedChanged += SelectedChanged;

        // update the selection state UI
        SelectedChanged(data.Selected);
    }

    public void OnSelected()
    {
        selected?.Invoke(this);
    }

    public string ItemSlotNameCase(int dataIndex)
    {
        return dataIndex switch
        {
            0 => "Main Weapon",
            1 => "Sub Weapon",
            2 => "Helmet",
            3 => "Upper Armor",
            4 => "Under Armor",
            _ => "Accesory",
        };
    }

    public Sprite ItemSlotImageCase(int dataIndex)
    {
        return dataIndex switch
        {
            0 => weaponImage,
            1 => weaponImage,
            2 => helmetImage,
            3 => upperImage,
            4 => bottomImage,
            _ => accesoryImage
        };
    }
}