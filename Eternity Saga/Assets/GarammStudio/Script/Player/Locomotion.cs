using UnityEngine;

public class Locomotion
{
    private PlayerManager _manager;
    public PlayerManager Manager { get { return _manager; } }
    private GroundedCheck _groundedCheck;
    private JumpAndGravity _jumpAndGravity;
    private Movement _movement;
    // player
    public float speed;
    public float animationBlend;
    public float targetRotation = 0.0f;
    public float rotationVelocity;
    public float verticalVelocity;
    public float terminalVelocity = 53.0f;

    public Locomotion(PlayerManager manager)
    {
        this._manager = manager;
        _groundedCheck = new GroundedCheck(this);
        _jumpAndGravity = new JumpAndGravity(this);
        _movement = new Movement(this);
    }

    public void OnStart()
    {
        _jumpAndGravity.OnStart();
        _movement.OnStart();
    }

    public void OnFixedUpdate()
    {
        _groundedCheck.OnUpdate();
        _jumpAndGravity.OnUpdate();
        _movement.OnFixedUpdate();
    }

    public void OnAnimatorMove()
    {
        _movement.OnAnimatorMove(_manager.AnimatorController.Animator);
    }

    public void OnDisable()
    {
        _jumpAndGravity.OnDisable();
        _movement.OnDisable();
    }
}