using UnityEngine;
public class GroundedCheck
{
    private Locomotion _locomotion;

    public GroundedCheck(Locomotion locomotion)
    {
        _locomotion = locomotion;
    }
    public void OnUpdate()
    {
        var spherePosition = new Vector3(_locomotion.Manager.transform.position.x,
            _locomotion.Manager.transform.position.y + _locomotion.Manager.prop.GroundedOffset,
            _locomotion.Manager.transform.position.z);
        _locomotion.Manager.prop.Grounded = Physics.CheckSphere(spherePosition,
            _locomotion.Manager.prop.GroundedRadius,
            _locomotion.Manager.prop.GroundLayers,
            QueryTriggerInteraction.Ignore);
        if (_locomotion.Manager.AnimatorController.HasAnimator)
            _locomotion.Manager.AnimatorController.Animator.SetBool(_locomotion.Manager.AnimatorController.AnimIDGrounded,
                _locomotion.Manager.prop.Grounded);
    }
}