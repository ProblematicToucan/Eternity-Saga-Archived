using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public int itemID;
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    [Multiline(5)] public string itemDescription;
}

public enum ItemType
{
    Consumable,
    Equipment,
    Quest,
    Misc
}