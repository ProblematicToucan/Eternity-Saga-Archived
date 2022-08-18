using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public ItemSO item;
    [field: SerializeField] public Image itemImage { get; private set; }
    [field: SerializeField] public TextMeshProUGUI itemCount { get; private set; }
    [field: SerializeField] public TextMeshProUGUI itemName { get; private set; }

    public void SetItem(ItemSO item)
    {
        this.item = item;
        itemImage.sprite = item.itemIcon;
        itemCount.text = 1.ToString();
        itemName.text = item.name;
    }

    [ContextMenu("SetItem")]
    private void Click()
    {
        SetItem(item);
    }
}
