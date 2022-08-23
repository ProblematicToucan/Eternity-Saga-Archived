using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class ItemSO : ScriptableObject
{
    public int itemID;
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    [Multiline(5)] public string itemDescription;
    [Range(1, 99)] public int MaximumStacks = 1;

#if UNITY_EDITOR
    protected void OnValidate()
    {
        var path = AssetDatabase.GetAssetPath(this);
        itemID = AssetDatabase.AssetPathToGUID(path).GetHashCode();
    }
#endif
}

public enum ItemType
{
    Consumable,
    Equipment,
    Quest,
    Misc
}