using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas myCanvas;
    [SerializeField] private GameObject inventorySlotFollower;
    [SerializeField] private GameObject abilitySlotFollower;

    private void Start()
    {
        UISlot.BeginDragAction += OnBeginDragAction;
        UISlot.DragAction += OnDragAction;
        UISlot.EndDragAction += OnEndDragAction;
    }

    private void OnDestroy()
    {
        UISlot.BeginDragAction -= OnBeginDragAction;
        UISlot.DragAction -= OnDragAction;
        UISlot.EndDragAction -= OnEndDragAction;
    }

    private void OnBeginDragAction(UISlot obj)
    {
        var slot = obj.GetType();
        if (slot == typeof(UIInventorySlot))
        {
            inventorySlotFollower.SetActive(true);
        }
        else
        {
            abilitySlotFollower.SetActive(true);
        }
    }

    private void OnEndDragAction()
    {
        inventorySlotFollower.SetActive(false);
        abilitySlotFollower.SetActive(false);
    }

    public void OnBeginDragInventorySlot(UISlot uiSlot)
    {
        var slot = (UIInventorySlot)uiSlot;
        Debug.Log(slot.gameObject.name);
    }

    public void OnDragAction(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Mouse.current.position.ReadValue(), myCanvas.worldCamera, out Vector2 pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }
}
