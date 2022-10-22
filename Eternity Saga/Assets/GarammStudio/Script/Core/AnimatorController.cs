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
    public int AnimIDIsInteracting { get; private set; }
    // RM IDs
    public int AnimIDRM { get; private set; }
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        AnimIDRM = Animator.StringToHash("RM");
        AnimIDSpeed = Animator.StringToHash("Speed");
        AnimIDGrounded = Animator.StringToHash("Grounded");
        AnimIDJump = Animator.StringToHash("Jump");
        AnimIDFreeFall = Animator.StringToHash("FreeFall");
        AnimIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        AnimIDCombatState = Animator.StringToHash("Combat State");
        AnimIDIsInteracting = Animator.StringToHash("IsInteracting");
        HasAnimator = Anim != null;


    }

    private void OnAnimatorMove()
    {
        var stateInfoBase0 = Anim.GetCurrentAnimatorStateInfo(0);
        var stateInfoBase1 = Anim.GetCurrentAnimatorStateInfo(1);
        if (stateInfoBase1.tagHash == AnimIDRM || stateInfoBase0.tagHash == AnimIDRM)
        {
            var movement = new Vector3(Anim.deltaPosition.x, -0.2f, Anim.deltaPosition.z);
            _manager.Locomotion.Controller.Move(movement);
            Anim.ApplyBuiltinRootMotion();
        }
    }

    public void PlayTargetedAnimation(string animationName, float transitionDuration = 0.0f)
    {
        Anim.SetBool(AnimIDIsInteracting, true);
        Anim.CrossFade(animationName, transitionDuration);
    }
}
