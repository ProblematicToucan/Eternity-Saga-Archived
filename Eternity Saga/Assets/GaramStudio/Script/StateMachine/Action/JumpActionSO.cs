using UnityEngine;

[CreateAssetMenu(menuName = "GarammStudio/StateMachine/Player/Action/Jump", fileName = "New JumpAction")]
public class JumpActionSO : ActionSO // Jump & gravity action.
{
    private bool isJumping = false;
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    internal override void OnEnterAction(PlayerManager manager)
    {
        base.OnEnterAction(manager);
        _manager.InputReader.jumpEvent += OnJump;
        _manager.InputReader.jumpCanceledEvent += OnJump;
        // reset our timeouts on start    
        _jumpTimeoutDelta = _manager.Prop.JumpTimeout;
        _fallTimeoutDelta = _manager.Prop.FallTimeout;
    }

    private void OnJump()
    {
        if (_manager.AnimatorController.Anim.GetBool(_manager.AnimatorController.AnimIDIsInteracting)) return;
        isJumping = !isJumping;
    }

    internal override void OnExitAction()
    {
        _manager.InputReader.jumpEvent -= OnJump;
        _manager.InputReader.jumpCanceledEvent -= OnJump;
    }

    internal override void OnFixedUpdateAction()
    {
        JumpAndGravity();
    }

    private void JumpAndGravity()
    {
        if (_manager.Prop.Grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = _manager.Prop.FallTimeout;

            // update animator if using character
            if (_manager.AnimatorController.HasAnimator)
            {
                _manager.AnimatorController.Anim.SetBool(_manager.AnimatorController.AnimIDJump, false);
                _manager.AnimatorController.Anim.SetBool(_manager.AnimatorController.AnimIDFreeFall, false);
            }

            // stop our velocity dropping infinitely when grounded
            if (_manager.Locomotion.verticalVelocity < 0.0f)
            {
                _manager.Locomotion.verticalVelocity = -2f;
            }

            // Jump
            if (isJumping && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _manager.Locomotion.verticalVelocity = Mathf.Sqrt(_manager.Prop.JumpHeight * -2f * _manager.Prop.Gravity);

                // update animator if using character
                if (_manager.AnimatorController.HasAnimator)
                {
                    _manager.AnimatorController.Anim.SetBool(_manager.AnimatorController.AnimIDJump, true);
                }
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = _manager.Prop.JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                // update animator if using character
                if (_manager.AnimatorController.HasAnimator)
                {
                    _manager.AnimatorController.Anim.SetBool(_manager.AnimatorController.AnimIDFreeFall, true);
                }
            }

            // if we are not grounded, do not jump
            isJumping = false;
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_manager.Locomotion.verticalVelocity < _manager.Locomotion.terminalVelocity)
        {
            _manager.Locomotion.verticalVelocity += _manager.Prop.Gravity * Time.deltaTime;
        }
    }

    internal override void OnUpdateAction()
    {

    }
}
