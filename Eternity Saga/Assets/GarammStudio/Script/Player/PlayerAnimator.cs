using UnityEngine;

public class PlayerAnimator
{
    private Animator animator;
    public Animator Animator { get { return animator; } }
    private bool hasAnimator;
    public bool HasAnimator { get { return hasAnimator; } }
    private PlayerManager _manager;

    // animation IDs
    private int _animIDSpeed;
    public int AnimIDSpeed { get { return _animIDSpeed; } }
    private int _animIDGrounded;
    public int AnimIDGrounded { get { return _animIDGrounded; } }
    private int _animIDJump;
    public int AnimIDJump { get { return _animIDJump; } }
    private int _animIDFreeFall;
    public int AnimIDFreeFall { get { return _animIDFreeFall; } }
    private int _animIDMotionSpeed;
    public int AnimIDMotionSpeed { get { return _animIDMotionSpeed; } }

    public void OnStart()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        animator = _manager.GetComponentInChildren<Animator>();
        hasAnimator = animator != null;
    }

    public PlayerAnimator(PlayerManager manager)
    {
        _manager = manager;
    }
}