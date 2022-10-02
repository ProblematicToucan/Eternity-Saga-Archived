using UnityEngine;

public abstract class ActionSO : ScriptableObject
{
    protected PlayerManager _manager;
    internal virtual void OnEnterAction(PlayerManager manager)
    {
        _manager = manager;
    }
    internal abstract void OnUpdateAction();
    internal abstract void OnFixedUpdateAction();
    internal abstract void OnExitAction();
}
