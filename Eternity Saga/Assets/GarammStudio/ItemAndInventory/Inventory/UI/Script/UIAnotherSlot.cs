using UnityEngine.EventSystems;

public class UIAnotherSlot : UISlot
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
}
