using UnityEngine;

[CreateAssetMenu(fileName = "new Inventory Database", menuName = "GarammStudio/Items Inventory/Inventory Database")]
public class ItemDatabaseSO : ScriptableObject
{
    [SerializeField] private ItemSO[] items;

    public ItemSO GetItemSOReferenceById(int _itemId)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemID == _itemId)
            {
                return items[i];
            }
        }
        return null;
    }

    public int GetIdReferenceByItemSO(ItemSO _itemSO)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == _itemSO)
            {
                return items[i].itemID;
            }
        }
        return -1;
    }
}