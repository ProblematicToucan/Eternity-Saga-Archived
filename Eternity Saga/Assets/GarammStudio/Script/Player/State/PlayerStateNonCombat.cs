using UnityEngine;

public class PlayerStateNonCombat : BaseState
{
    private Locomotion locomotion;
    public PlayerStateNonCombat(PlayerManager manager, StateFactory factory) : base(manager, factory)
    {
        locomotion = new Locomotion(manager);
    }

    public override void OnEnter()
    {
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

}
