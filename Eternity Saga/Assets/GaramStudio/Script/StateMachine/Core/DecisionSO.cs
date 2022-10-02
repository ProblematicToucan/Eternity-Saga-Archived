using UnityEngine;

public abstract class DecisionSO : ScriptableObject
{
    public abstract bool Decise(PlayerManager manager);
}
