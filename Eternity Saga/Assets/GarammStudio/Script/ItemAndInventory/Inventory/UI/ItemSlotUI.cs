using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

/// <summary>
/// Refference class to the item slot UI.
/// </summary>
public class ItemSlotUI : MonoBehaviour
{
    public ItemSO Item;
    [field: SerializeField] public Image ItemImage { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ItemCount { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ItemName { get; private set; }

    public async void UpdateDisplay(ItemSO _item, int _amount)
    {
        Item = _item;
        ItemImage.sprite = _item.ItemIcon;
        ItemCount.text = _amount.ToString();
        ItemName.text = _item.name;
        await Task.Yield();
    }
}
