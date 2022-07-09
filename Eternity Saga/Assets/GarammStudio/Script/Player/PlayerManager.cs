using UnityEngine;

public class PlayerManager : Character
{
    [Expandable]
    public CharacterStat stat;

    private void Start()
    {
        // Test mendapatkan data stat character strength
        Debug.Log(stat.strength.Value);
    }
}