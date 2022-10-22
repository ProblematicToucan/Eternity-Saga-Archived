using UnityEngine;

[CreateAssetMenu(fileName = "new Inventory Database", menuName = "GarammStudio/Items Inventory/Inventory Database")]
public class ItemDatabaseSO : ScriptableObject
{
    [SerializeField] private ItemSO[] items;

    /// <summary>
    /// Get ItemSO by item id from databse.
    /// </summary>
    /// <param name="_itemId">id of the item.</param>
    /// <returns>ItemSO.</returns>
    public ItemSO GetItemSOReferenceById(int _itemId)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].ItemId == _itemId)
            {
                return items[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Get item id by ItemSO from database.
    /// </summary>
    /// <param name="_itemSO"></param>
    /// <returns></returns>
    public int GetIdReferenceByItemSO(ItemSO _itemSO)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == _itemSO)
            {
                return items[i].ItemId;
            }
        }
        return -1;
    }
}