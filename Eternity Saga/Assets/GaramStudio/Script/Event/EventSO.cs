using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "GaramStudio/Event", order = 52)]
public class EventSO : ScriptableObject
{
    private List<Event> events = new List<Event>();

    public void Raise()
    {
        for (int i = events.Count - 1; i >= 0; i--)
        {
            events[i].OnEventRaised();
        }
    }

    public void RegisterListener(Event _event)
    {
        events.Add(_event);
    }

    public void UnregisterListener(Event _event)
    {
        events.Remove(_event);
    }
}