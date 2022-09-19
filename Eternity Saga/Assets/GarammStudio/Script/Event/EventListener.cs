using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [field: SerializeField] public List<Event> Events { get; private set; }
    private void OnEnable()
    {
        for (int i = Events.Count - 1; i >= 0; i--)
        {
            Events[i].OnEnable();
        }
    }

    private void OnDisable()
    {
        for (int i = Events.Count - 1; i >= 0; i--)
        {
            Events[i].OnDisable();
        }
    }

    public void OnEventRaised()
    {
        for (int i = Events.Count - 1; i >= 0; i--)
        {
            Events[i].OnEventRaised();
        }
    }
}

[System.Serializable]
public class Event
{
    public EventSO eventChanel;
    public UnityEvent response;
    public void OnEnable()
    {
        eventChanel?.RegisterListener(this);
    }

    public void OnDisable()
    {
        eventChanel?.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}