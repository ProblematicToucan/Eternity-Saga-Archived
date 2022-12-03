using System.Collections.Generic;
using UnityEngine;

#region GameEventSO
[CreateAssetMenu(fileName = "New Game Event", menuName = "GarammStudio/Game Event", order = 52)]
public class GameEventSO : ScriptableObject
{
    private readonly List<IGameEventListener> eventListeners = new();

    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised();
        }
    }

    #region ListenerRegistration
    public void RegisterListener(IGameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(IGameEventListener listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
    #endregion
}
#endregion
