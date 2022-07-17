using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private PlayerManager _manager;
    private Animator _animator;
    public Animator Animator { get { return _animator; } }
    private bool hasAnimator;
    public bool HasAnimator { get { return hasAnimator; } }

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
    private int _animIDCombatState;
    public int AnimIDCombatState { get { return _animIDCombatState; } }
    // RM IDs
    private int _animIDRM;
    public int AnimIDRM { get { return _animIDRM; } }
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animIDRM = Animator.StringToHash("RM");
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDCombatState = Animator.StringToHash("Combat State");
        hasAnimator = _animator != null;
    }

    private void OnAnimatorMove()
    {
        var stateInfoBase = _animator.GetCurrentAnimatorStateInfo(1);
        if (stateInfoBase.tagHash == _animIDRM)
        {
            _manager.stateController.OnAnimatorMove();
            _animator.ApplyBuiltinRootMotion();
        }
    }
}
