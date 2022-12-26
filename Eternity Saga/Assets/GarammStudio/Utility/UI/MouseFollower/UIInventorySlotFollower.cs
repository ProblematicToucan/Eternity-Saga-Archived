using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlotFollower : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemCount;

    public void SetDisplay(UIInventorySlot inventorySlot)
    {
        itemIcon.sprite = inventorySlot.GetSlotSprite();
        itemCount.text = inventorySlot.GetItemCount();
    }
}
