using UnityEngine;

[CreateAssetMenu(fileName = "new Consumable Item", menuName = "GarammStudio/Items Inventory/Item/Consumable Item")]
public class ConsumableItemSO : ItemSO
{
    private void Awake()
    {
        ItemType = ItemType.Consumable;
    }
}
