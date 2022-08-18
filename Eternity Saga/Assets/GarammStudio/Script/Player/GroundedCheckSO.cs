using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerGroundedCheck", menuName = "GarammStudio/Player/GroundedCheck")]
public class GroundedCheckSO : ScriptableObject
{
    private PlayerManager _manager;

    public void OnStart(PlayerManager manager)
    {
        _manager = manager;
    }

    public void OnUpdate()
    {
        var spherePosition = new Vector3(_manager.transform.position.x,
            _manager.transform.position.y + _manager.Prop.GroundedOffset,
            _manager.transform.position.z);
        _manager.Prop.Grounded = Physics.CheckSphere(spherePosition,
            _manager.Prop.GroundedRadius,
            _manager.Prop.GroundLayers,
            QueryTriggerInteraction.Ignore);
        if (_manager.AnimatorController.HasAnimator)
            _manager.AnimatorController.Anim.SetBool(_manager.AnimatorController.AnimIDGrounded,
                _manager.Prop.Grounded);
    }
}