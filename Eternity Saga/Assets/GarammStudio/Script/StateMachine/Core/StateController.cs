using UnityEngine;

[System.Serializable]
public class StateController
{
    public BaseState currentState;
    [SerializeField]
    private StateFactory _factory;
    private PlayerManager _manager;

    public StateController(PlayerManager manager)
    {
        _manager = manager;
        _factory = new StateFactory(manager);
    }
    public void OnStart()
    {
        currentState = _factory.NonCombat();
        currentState.OnEnter();
    }
    public void OnUpdate()
    {
        currentState.OnUpdate();
    }
    public void OnFixedUpdate()
    {
        currentState.OnFixedUpdate();
    }
    public void OnDisable()
    {
        currentState.OnExit();
    }
}
