using UnityEngine;

public class JumpAndGravity
{
    private Locomotion _locomotion;
    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    private bool isJumping = false;
    public JumpAndGravity(Locomotion locomotion)
    {
        _locomotion = locomotion;
    }

    public void OnStart()
    {
        _locomotion.Manager.inputReader.jumpEvent += OnJump;
        _locomotion.Manager.inputReader.jumpCanceledEvent += OnJump;
        // reset our timeouts on start
        _jumpTimeoutDelta = _locomotion.Manager.prop.JumpTimeout;
        _fallTimeoutDelta = _locomotion.Manager.prop.FallTimeout;
    }

    private void OnJump()
    {
        isJumping = !isJumping;
    }

    public void OnUpdate()
    {
        if (_locomotion.Manager.prop.Grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = _locomotion.Manager.prop.FallTimeout;

            // update animator if using character
            if (_locomotion.Manager.AnimatorController.HasAnimator)
            {
                _locomotion.Manager.AnimatorController.Animator.SetBool(_locomotion.Manager.AnimatorController.AnimIDJump, false);
                _locomotion.Manager.AnimatorController.Animator.SetBool(_locomotion.Manager.AnimatorController.AnimIDFreeFall, false);
            }

            // stop our velocity dropping infinitely when grounded
            if (_locomotion.verticalVelocity < 0.0f)
            {
                _locomotion.verticalVelocity = -2f;
            }

            // Jump
            if (isJumping && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _locomotion.verticalVelocity = Mathf.Sqrt(_locomotion.Manager.prop.JumpHeight * -2f * _locomotion.Manager.prop.Gravity);

                // update animator if using character
                if (_locomotion.Manager.AnimatorController.HasAnimator)
                {
                    _locomotion.Manager.AnimatorController.Animator.SetBool(_locomotion.Manager.AnimatorController.AnimIDJump, true);
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
            _jumpTimeoutDelta = _locomotion.Manager.prop.JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                // update animator if using character
                if (_locomotion.Manager.AnimatorController.HasAnimator)
                {
                    _locomotion.Manager.AnimatorController.Animator.SetBool(_locomotion.Manager.AnimatorController.AnimIDFreeFall, true);
                }
            }

            // if we are not grounded, do not jump
            isJumping = false;
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_locomotion.verticalVelocity < _locomotion.terminalVelocity)
        {
            _locomotion.verticalVelocity += _locomotion.Manager.prop.Gravity * Time.deltaTime;
        }
    }

    public void OnDisable()
    {
        _locomotion.Manager.inputReader.jumpEvent -= OnJump;
        _locomotion.Manager.inputReader.jumpCanceledEvent -= OnJump;
    }
}