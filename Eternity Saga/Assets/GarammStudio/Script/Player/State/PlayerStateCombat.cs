using UnityEngine;

public class PlayerStateCombat : BaseState
{
    public PlayerStateCombat(PlayerManager manager, StateFactory factory) : base(manager, factory)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Combat State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Combat State");   
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnUpdate()
    {
        
    }
}
