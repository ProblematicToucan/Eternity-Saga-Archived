using UnityEngine;

[CreateAssetMenu(fileName = "new StateController", menuName = "GarammStudio/Player/StateMachine/StateController")]
public class StateControllerSO : ScriptableObject
{
    private PlayerManager _manager;
    public StateSO currentState;
    public StateSO remainingState;
    public void OnStart(PlayerManager manager)
    {
        _manager = manager;
        currentState.OnEnterState(_manager);
    }
    public void OnUpdate()
    {
        currentState.OnUpdateState();
    }
    public void OnFixedUpdate()
    {
        currentState.OnFixedUpdateState();
    }

    public void OnExit()
    {
        OnExitState();
    }
    public void TransitionToState(StateSO nextState)
    {
        if (nextState != remainingState)
        {
            OnExitState();
            currentState = nextState;
            currentState.OnEnterState(_manager);
        }
    }

    private void OnExitState()
    {
        currentState.OnExitState();
    }
}
