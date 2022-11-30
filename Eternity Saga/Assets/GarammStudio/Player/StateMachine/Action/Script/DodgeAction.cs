using UnityEngine;

[CreateAssetMenu(menuName = "GarammStudio/Player/StateMachine/Action/Dodge", fileName = "New DodgeAction")]
public class DodgeAction : ActionSO
{
    internal override void OnEnterAction(PlayerManager manager)
    {
        base.OnEnterAction(manager);
        _manager.InputReader.DodgeEvent += Dodge;
    }
    internal override void OnExitAction()
    {

    }

    internal override void OnFixedUpdateAction()
    {

    }

    internal override void OnUpdateAction()
    {

    }

    private void Dodge()
    {
        if (_manager.AnimatorController.Anim.GetBool(_manager.AnimatorController.AnimIDIsInteracting)) return;
        if (!_manager.prop.Grounded) return;
        if (_manager.Locomotion.speed > 1) _manager.AnimatorController.PlayTargetedAnimation("Roll", 0.1f);
        else _manager.AnimatorController.PlayTargetedAnimation("Stepback", 0.05f);
    }
}