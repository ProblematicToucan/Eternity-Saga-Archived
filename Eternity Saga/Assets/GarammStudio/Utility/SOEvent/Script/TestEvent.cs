using UnityEngine;
using UnityEngine.Events;

public class TestEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent<TestEvent> response;

    [ContextMenu("Raise")]
    public void Raise()
    {
        response?.Invoke(this);
    }

    public void GetRaised(TestEvent listener)
    {
        Debug.Log(listener.gameObject.name);
    }
}
