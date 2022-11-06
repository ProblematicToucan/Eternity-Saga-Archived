using GarammStudio.Script.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new InputReader", menuName = "GarammStudio/Input/InputReader")]
public class InputReader : ScriptableObject, GlobalControls.IGameplayActions, GlobalControls.INonGameplayActions
{
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction JumpCanceledEvent = delegate { };
    public event UnityAction DodgeEvent = delegate { };
    private GlobalControls controls;

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new GlobalControls();
            controls.Gameplay.SetCallbacks(this);
            controls.NonGameplay.SetCallbacks(this);
        }
        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    private void DisableAllInput()
    {
        controls.Gameplay.Disable();
        controls.NonGameplay.Disable();
    }

    private void EnableGameplayInput()
    {
        controls.Gameplay.Enable();
        controls.NonGameplay.Disable();
    }

    public void OnCameraLook(InputAction.CallbackContext context)
    {

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) JumpEvent.Invoke();
        if (context.phase == InputActionPhase.Canceled) JumpCanceledEvent.Invoke();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnScreenPosition(InputAction.CallbackContext context)
    {

    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) DodgeEvent.Invoke();
    }
}
