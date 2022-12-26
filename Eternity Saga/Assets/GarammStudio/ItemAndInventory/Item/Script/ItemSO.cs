using UnityEditor;
using UnityEngine;

public class ItemSO : ScriptableObject
{
    public int ItemID { get; private set; }
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public Sprite ItemIcon { get; private set; }
    [field: SerializeField, Multiline(5)] public string ItemDescription { get; private set; }
    [field: SerializeField] public bool IsStackable { get; private set; }

    private void OnValidate()
    {
        var path = AssetDatabase.GetAssetPath(this);
        ItemID = AssetDatabase.AssetPathToGUID(path).GetHashCode();
    }
}

public enum ItemType
{
    Stuff,
    Equipment,
    Consumable
}
