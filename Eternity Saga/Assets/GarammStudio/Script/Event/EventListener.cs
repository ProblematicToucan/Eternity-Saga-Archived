using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [SerializeField] private List<Event> events;
    private void OnEnable()
    {
        for (int i = events.Count - 1; i >= 0; i--)
        {
            events[i].OnEnable();
        }
    }

    private void OnDisable()
    {
        for (int i = events.Count - 1; i >= 0; i--)
        {
            events[i].OnDisable();
        }
    }

    public void OnEventRaised()
    {
        for (int i = events.Count - 1; i >= 0; i--)
        {
            events[i].OnEventRaised();
        }
    }
}

[System.Serializable]
public class Event
{
    public EventSO eventSO;
    public UnityEvent response;
    public void OnEnable()
    {
        eventSO?.RegisterListener(this);
    }

    public void OnDisable()
    {
        eventSO?.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response?.Invoke();
    }
}