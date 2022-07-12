using UnityEngine;

public class PlayerStateDead : BaseState
{
    public PlayerStateDead(PlayerManager manager, StateFactory factory) : base(manager, factory)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("PlayerStateDead.OnEnter");
    }

    public override void OnExit()
    {
        Debug.Log("PlayerStateDead.OnExit");
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {

    }
}
