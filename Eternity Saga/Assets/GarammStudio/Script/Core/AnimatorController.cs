using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private PlayerManager _manager;
    [field: SerializeField] public Animator Anim { get; private set; }
    [field: SerializeField] public bool HasAnimator { get; private set; }

    // animation IDs
    public int AnimIDSpeed { get; private set; }
    public int AnimIDGrounded { get; private set; }
    public int AnimIDJump { get; private set; }
    public int AnimIDFreeFall { get; private set; }
    public int AnimIDMotionSpeed { get; private set; }
    public int AnimIDCombatState { get; private set; }
    // RM IDs
    public int AnimIDRM { get; private set; }
    void Start()
    {
        Anim = GetComponent<Animator>();
        AnimIDRM = Animator.StringToHash("RM");
        AnimIDSpeed = Animator.StringToHash("Speed");
        AnimIDGrounded = Animator.StringToHash("Grounded");
        AnimIDJump = Animator.StringToHash("Jump");
        AnimIDFreeFall = Animator.StringToHash("FreeFall");
        AnimIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        AnimIDCombatState = Animator.StringToHash("Combat State");
        HasAnimator = Anim != null;
    }

    private void OnAnimatorMove()
    {
        var stateInfoBase = Anim.GetCurrentAnimatorStateInfo(1);
        if (stateInfoBase.tagHash == AnimIDRM)
        {
            _manager.stateController.OnAnimatorMove();
            Anim.ApplyBuiltinRootMotion();
        }
    }
}
