using UnityEngine;

[CreateAssetMenu(menuName = "GarammStudio/StateMachine/Player/Action/Move", fileName = "New MoveAction")]
public class MoveActionSO : ActionSO
{
    private Vector2 _movement;
    internal override void OnEnterAction(PlayerManager manager)
    {
        base.OnEnterAction(manager);
        _manager.InputReader.moveEvent += OnMove;
    }

    private void OnMove(Vector2 arg0)
    {
        _movement = arg0;
    }

    internal override void OnExitAction()
    {
        _manager.InputReader.moveEvent -= OnMove;
    }

    internal override void OnFixedUpdateAction()
    {
        Movement();
    }

    private void Movement()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        var targetSpeed = _movement.magnitude > .8f ? _manager.Stat.SprintSpeed : _manager.Stat.WalkSpeed;
        if (_manager.AnimatorController.Anim.GetBool(_manager.AnimatorController.AnimIDIsInteracting))
            targetSpeed = 0;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (_movement == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        var currentHorizontalSpeed = new Vector3(_manager.Locomotion.Controller.velocity.x,
            0.0f,
            _manager.Locomotion.Controller.velocity.z).magnitude;

        var speedOffset = 0.1f;
        // var inputMagnitude = _locomotion.Manager.inputReader.AnalogMovement ? _movement.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _manager.Locomotion.speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed /* * inputMagnitude */,
                Time.deltaTime * _manager.Prop.SpeedChangeRate);

            // round speed to 3 decimal places
            _manager.Locomotion.speed = Mathf.Round(_manager.Locomotion.speed * 1000f) / 1000f;
        }
        else
        {
            _manager.Locomotion.speed = targetSpeed;
        }

        _manager.Locomotion.animationBlend = Mathf.Lerp(_manager.Locomotion.animationBlend,
            targetSpeed,
            Time.deltaTime * _manager.Prop.SpeedChangeRate);
        if (_manager.Locomotion.animationBlend < 0.01f) _manager.Locomotion.animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(_movement.x, 0.0f, _movement.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_movement != Vector2.zero)
        {
            _manager.Locomotion.targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _manager.mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(_manager.transform.eulerAngles.y,
                _manager.Locomotion.targetRotation,
                ref _manager.Locomotion.rotationVelocity,
                _manager.Prop.RotationSmoothTime);

            // rotate to face input direction relative to camera position
            _manager.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _manager.Locomotion.targetRotation, 0.0f) * Vector3.forward;

        // move the player
        if (!_manager.AnimatorController.Anim.GetBool(_manager.AnimatorController.AnimIDIsInteracting))
        {
            _manager.Locomotion.Controller.Move(targetDirection.normalized * (_manager.Locomotion.speed * Time.deltaTime) +
                new Vector3(0.0f, _manager.Locomotion.verticalVelocity, 0.0f) * Time.deltaTime);
        }

        // update animator if using character
        if (_manager.AnimatorController.HasAnimator)
        {
            _manager.AnimatorController.Anim.SetFloat(_manager.AnimatorController.AnimIDSpeed, _manager.Locomotion.animationBlend);
            _manager.AnimatorController.Anim.SetFloat(_manager.AnimatorController.AnimIDMotionSpeed, 1);
        }
    }

    internal override void OnUpdateAction()
    {

    }
}
