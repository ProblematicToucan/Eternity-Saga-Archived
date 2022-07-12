using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateFactory
{
    private string currentState;
    private readonly Dictionary<States, BaseState> _states = new Dictionary<States, BaseState>();
    public StateFactory(PlayerManager manager)
    {
        _states[States.Combat] = new PlayerStateCombat(manager, this);
        _states[States.NonCombat] = new PlayerStateNonCombat(manager, this);
        _states[States.Dead] = new PlayerStateDead(manager, this);
    }

    public BaseState Combat()
    {
        currentState = States.Combat.ToString();
        return _states[States.Combat];
    }
    public BaseState NonCombat()
    {
        currentState = States.NonCombat.ToString();
        return _states[States.NonCombat];
    }
    public BaseState Dead()
    {
        currentState = States.Dead.ToString();
        return _states[States.Dead];
    }
}

public enum States
{
    NonCombat = 100,
    Combat = 200,
    Dead = 300
}