using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private List<GameEvent> gameEvents;

    private void OnEnable()
    {
        for (int i = gameEvents.Count - 1; i >= 0; i--)
        {
            gameEvents[i].OnEnable();
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < gameEvents.Count; i++)
        {
            gameEvents[i].OnDisable();
        }
    }
}

[System.Serializable]
public class GameEvent : IGameEventListener
{
    [SerializeField] private GameEventSO @event;
    [SerializeField] private UnityEvent response;
    public void OnEnable()
    {
        if (@event != null)
        {
            @event.RegisterListener(this);
        }
    }

    public void OnDisable()
    {
        @event.UnregisterListener(this);
    }
    public void OnEventRaised()
    {
        response?.Invoke();
    }
}
