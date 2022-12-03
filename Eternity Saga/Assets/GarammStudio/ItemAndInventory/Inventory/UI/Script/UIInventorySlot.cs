using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : UISlot, IDropHandler
{
    public override void OnBeginDrag(PointerEventData eventData)
    {
        BeginDrag(this);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        Drag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name);
    }
}
