using UnityEngine;

[CreateAssetMenu(menuName = "GarammStudio/StateMachine/Player/State", fileName = "New PlayerState")]
public class StateSO : ScriptableObject
{
    private PlayerManager _manager;
    public ActionSO[] actions;
    public Transition[] transitions;
    public Color stateGizmoColor = Color.blue;
    public void OnEnterState(PlayerManager manager)
    {
        _manager = manager;
        // Debug.Log("Entering state: " + name);
        if (actions == null) return;
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].OnEnterAction(manager);
        }
    }
    public void OnUpdateState()
    {
        if (actions == null) return;
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].OnUpdateAction();
        }
        CheckForTransitions(_manager);
    }
    public void OnFixedUpdateState()
    {
        if (actions == null) return;
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].OnFixedUpdateAction();
        }
    }
    public void OnExitState()
    {
        if (actions == null) return;
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].OnExitAction();
        }
    }
    private void CheckForTransitions(PlayerManager manager)
    {
        if (transitions == null) return;
        for (int i = 0; i < transitions.Length; i++)
        {
            var decision = transitions[i].decision.Decise(manager);
            if (decision) manager.StateController.TransitionToState(transitions[i].trueState);
            else manager.StateController.TransitionToState(transitions[i].falseState);
        }
    }
}
