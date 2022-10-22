using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class ItemSO : ScriptableObject
{
    public int ItemId;
    public ItemType ItemType;
    public string ItemName;
    public Sprite ItemIcon;
    public ItemBuff[] Buffs;
    [Multiline(5)] public string ItemDescription;
    [Range(1, 99)] public int MaximumStacks = 1;

#if UNITY_EDITOR
    protected void OnValidate()
    {
        var path = AssetDatabase.GetAssetPath(this);
        ItemId = AssetDatabase.AssetPathToGUID(path).GetHashCode();
    }
#endif

    public Item GenerateItem()
    {
        var newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    [HideInInspector] public int Id;
    public string Name;
    public ItemBuff[] Buffs;

    public Item()
    {
        Id = -1;
        Name = null;
        Buffs = null;
    }

    /// <summary>
    /// Constructor to create item object with referencing to ItemSO.
    /// </summary>
    /// <param name="_itemSO">itemSO</param>
    public Item(ItemSO _itemSO)
    {
        Id = _itemSO.ItemId;
        Name = _itemSO.ItemName;
        Buffs = new ItemBuff[_itemSO.Buffs.Length];
        for (int i = Buffs.Length - 1; i >= 0; i--)
        {
            Buffs[i] = new ItemBuff(_itemSO.Buffs[i].Min, _itemSO.Buffs[i].Max)
            {
                Attribute = _itemSO.Buffs[i].Attribute
            };
        }
    }

    /// <summary>
    /// Constructor to create item object with referencing to ItemSO and buff array.
    /// </summary>
    /// <param name="_itemSO">itemSO</param>
    /// <param name="_buffs">buff array</param>
    public Item(ItemSO _itemSO, ItemBuff[] _buffs)
    {
        Id = _itemSO.ItemId;
        Name = _itemSO.ItemName;
        Buffs = _buffs;
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attribute Attribute;
    [ReadOnly] public int Value;
    public int Min, Max;

    /// <summary>
    /// Constructor ItemBuff with minimum and maximum buff value.
    /// </summary>
    /// <param name="_min">Minimum buff value</param>
    /// <param name="_max">Maximum buff value</param>
    public ItemBuff(int _min, int _max)
    {
        Min = _min;
        Max = _max;
        GenerateValue();
    }

    public void GenerateValue()
    {
        Value = Random.Range(Min, Max);
    }
}

public enum Attribute
{
    strength,
    intelligence,
    agility,
    vitality,
    dexterity
}

public enum ItemType
{
    Consumable,
    Equipment,
    Quest,
    Misc
}