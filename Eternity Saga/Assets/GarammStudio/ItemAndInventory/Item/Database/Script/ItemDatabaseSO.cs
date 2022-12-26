using UnityEngine;

[CreateAssetMenu(fileName = "new Item Database", menuName = "GarammStudio/Item Database")]
public class ItemDatabaseSO : ScriptableObject
{
    [SerializeField] private ItemSO[] items;

    public ItemSO GetItemSO(int itemID)
    {
        foreach (var item in items)
        {
            if (item.ItemID == itemID)
            {
                return item;
            }
        }
        return null;
    }
}
