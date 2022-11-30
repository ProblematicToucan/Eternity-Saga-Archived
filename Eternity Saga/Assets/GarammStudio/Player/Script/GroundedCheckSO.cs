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
            _manager.transform.position.y + _manager.prop.GroundedOffset,
            _manager.transform.position.z);
        _manager.prop.Grounded = Physics.CheckSphere(spherePosition,
            _manager.prop.GroundedRadius,
            _manager.prop.GroundLayers,
            QueryTriggerInteraction.Ignore);
        if (_manager.AnimatorController.HasAnimator)
            _manager.AnimatorController.Anim.SetBool(_manager.AnimatorController.AnimIDGrounded,
                _manager.prop.Grounded);
    }
}