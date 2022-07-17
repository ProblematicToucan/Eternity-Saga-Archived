public class PlayerStateNonCombat : BaseState
{
    private Locomotion locomotion;
    public PlayerStateNonCombat(PlayerManager manager, StateFactory factory) : base(manager, factory)
    {
        locomotion = new Locomotion(manager);
    }

    public override void OnEnter()
    {
        _manager.AnimatorController.Animator.SetBool(_manager.AnimatorController.AnimIDCombatState, false);
        locomotion.OnStart();
    }

    public override void OnExit()
    {
        locomotion.OnDisable();
    }

    public override void OnUpdate()
    {

    }
    public override void OnFixedUpdate()
    {
        locomotion.OnFixedUpdate();
    }

    public override void OnAnimatorMove()
    {
        locomotion.OnAnimatorMove();
    }
}
