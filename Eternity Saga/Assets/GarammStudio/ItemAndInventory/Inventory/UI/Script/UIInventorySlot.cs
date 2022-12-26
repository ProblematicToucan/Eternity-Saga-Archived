using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : UISlot, IDropHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemCount;
    private int itemID;

    public int GetItemID() => itemID;
    public Sprite GetSlotSprite() => itemIcon.sprite;
    public string GetItemCount() => itemCount.text;

    public void SetSlot(int itemID, Sprite image, int count)
    {
        this.itemID = itemID;
        if (itemID == 0)
        {
            itemIcon.color = new Color(1, 1, 1, 0);
            itemCount.gameObject.SetActive(false);
        }
        else
        {
            itemIcon.color = Color.white;
            itemCount.gameObject.SetActive(true);
        }
        itemCount.text = count.ToString();
        itemIcon.sprite = image;

    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (itemID == 0) return;
        BeginDrag(this);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (itemID == 0) return;
        Drag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.TryGetComponent<UIInventorySlot>(out var draggedSlot);
        if (draggedSlot == null || draggedSlot.GetItemID() == 0) return;

        var tempSlot = new { itemID, sprite = GetSlotSprite(), count = int.Parse(GetItemCount()) };

        SetSlot(draggedSlot.GetItemID(), draggedSlot.GetSlotSprite(), int.Parse(draggedSlot.GetItemCount()));

        draggedSlot.SetSlot(tempSlot.itemID, tempSlot.sprite, tempSlot.count);
    }
}
