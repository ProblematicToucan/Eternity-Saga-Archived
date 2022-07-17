using UnityEngine;

public class Movement
{
    private CharacterController _controller;
    private Locomotion _locomotion;
    private Vector2 _movement;
    private bool isSprinting = true;

    public Movement(Locomotion locomotion)
    {
        _locomotion = locomotion;
    }

    public void OnStart()
    {
        _controller = _locomotion.Manager.GetComponent<CharacterController>();
        _locomotion.Manager.inputReader.moveEvent += OnMove;
    }

    private void OnMove(Vector2 arg0)
    {
        _movement = arg0;
    }

    public void OnFixedUpdate()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        var targetSpeed = isSprinting ? _locomotion.Manager.stat.speed.Value * 1.5f :
            _locomotion.Manager.stat.speed.Value;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (_movement == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        var currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        var speedOffset = 0.1f;
        var inputMagnitude = _locomotion.Manager.inputReader.AnalogMovement ? _movement.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _locomotion.speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * _locomotion.Manager.prop.SpeedChangeRate);

            // round speed to 3 decimal places
            _locomotion.speed = Mathf.Round(_locomotion.speed * 1000f) / 1000f;
        }
        else
        {
            _locomotion.speed = targetSpeed;
        }

        _locomotion.animationBlend = Mathf.Lerp(_locomotion.animationBlend,
            targetSpeed,
            Time.deltaTime * _locomotion.Manager.prop.SpeedChangeRate);
        if (_locomotion.animationBlend < 0.01f) _locomotion.animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(_movement.x, 0.0f, _movement.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_movement != Vector2.zero)
        {
            _locomotion.targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _locomotion.Manager.mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(_locomotion.Manager.transform.eulerAngles.y,
                _locomotion.targetRotation,
                ref _locomotion.rotationVelocity,
                _locomotion.Manager.prop.RotationSmoothTime);

            // rotate to face input direction relative to camera position
            _locomotion.Manager.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _locomotion.targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _controller.Move(targetDirection.normalized * (_locomotion.speed * Time.deltaTime) +
                         new Vector3(0.0f, _locomotion.verticalVelocity, 0.0f) * Time.deltaTime);

        // update animator if using character
        if (_locomotion.Manager.AnimatorController.HasAnimator)
        {
            _locomotion.Manager.AnimatorController.Animator.SetFloat(_locomotion.Manager.AnimatorController.AnimIDSpeed, _locomotion.animationBlend);
            _locomotion.Manager.AnimatorController.Animator.SetFloat(_locomotion.Manager.AnimatorController.AnimIDMotionSpeed, inputMagnitude);
        }
    }

    public void OnAnimatorMove(Animator animator)
    {
        _controller.Move(animator.deltaPosition);
    }

    public void OnDisable()
    {
        _locomotion.Manager.inputReader.moveEvent -= OnMove;
    }
}