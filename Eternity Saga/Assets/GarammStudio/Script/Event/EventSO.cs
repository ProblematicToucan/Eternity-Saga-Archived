using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "GarammStudio/Event", order = 52)]
public class EventSO : ScriptableObject
{
    private List<Event> listeners = new List<Event>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(Event _event)
    {
        listeners.Add(_event);
    }

    public void UnregisterListener(Event _event)
    {
        listeners.Remove(_event);
    }
}