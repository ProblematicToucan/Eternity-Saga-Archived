using System.Threading.Tasks;

public abstract class BaseState
{
    protected PlayerManager _manager;
    protected StateFactory _factory;

    protected BaseState(PlayerManager manager, StateFactory factory)
    {
        _manager = manager;
        _factory = factory;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnAnimatorMove();
    public async void TransitionToState(BaseState nexState)
    {
        if (_manager.stateController.currentState == nexState) return;
        OnExit();
        nexState.OnEnter();
        _manager.stateController.currentState = nexState;
        await Task.Yield();
    }
}