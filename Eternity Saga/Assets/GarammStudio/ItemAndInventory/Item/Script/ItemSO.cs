using UnityEngine;

public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    [Multiline(5)] public string itemDescription;
}
