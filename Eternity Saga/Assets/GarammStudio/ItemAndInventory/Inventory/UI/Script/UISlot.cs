using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UISlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static event Action<UISlot> BeginDragAction = delegate { };
    public static event Action<PointerEventData> DragAction = delegate { };
    public static event Action EndDragAction = delegate { };
    public abstract void OnBeginDrag(PointerEventData eventData);

    public abstract void OnDrag(PointerEventData eventData);

    public abstract void OnEndDrag(PointerEventData eventData);

    public void BeginDrag(UISlot slot) => BeginDragAction?.Invoke(slot);
    public void Drag(PointerEventData eventData) => DragAction?.Invoke(eventData);
    public void EndDrag() => EndDragAction?.Invoke();
}
