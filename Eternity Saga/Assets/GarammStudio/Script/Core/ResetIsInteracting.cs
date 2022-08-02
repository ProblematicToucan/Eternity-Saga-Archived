using UnityEngine;

public class ResetIsInteracting : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var AnimIDIsInteracting = Animator.StringToHash("IsInteracting");
        animator.SetBool(AnimIDIsInteracting, false);
    }
}