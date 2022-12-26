using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIMouseFollower : MonoBehaviour
{
    [SerializeField] private UIInventorySlotFollower inventorySlotFollower;
    [SerializeField] private UIAbilitySlotFollower abilitySlotFollower;
    private Canvas myCanvas;

    private void Start()
    {
        myCanvas = gameObject.GetComponentInParent<Canvas>();

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
            inventorySlotFollower.SetDisplay(obj as UIInventorySlot);
            inventorySlotFollower.gameObject.SetActive(true);
        }
        else
        {
            abilitySlotFollower.gameObject.SetActive(true);
        }
    }

    private void OnEndDragAction()
    {
        inventorySlotFollower.gameObject.SetActive(false);
        abilitySlotFollower.gameObject.SetActive(false);
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
