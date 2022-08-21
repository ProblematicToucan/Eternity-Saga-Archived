using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Refference class to the item slot UI.
/// </summary>
public class ItemSlotUI : MonoBehaviour
{
    public ItemSO item;
    [field: SerializeField] public Image itemImage { get; private set; }
    [field: SerializeField] public TextMeshProUGUI itemCount { get; private set; }
    [field: SerializeField] public TextMeshProUGUI itemName { get; private set; }

    public void UpdateDisplay(ItemSO _item, int _amount)
    {
        item = _item;
        itemImage.sprite = _item.itemIcon;
        itemCount.text = _amount.ToString();
        itemName.text = _item.name;
    }
}
