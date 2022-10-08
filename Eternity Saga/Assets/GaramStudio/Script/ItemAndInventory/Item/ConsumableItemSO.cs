using UnityEngine;

[CreateAssetMenu(fileName = "new Consumable Item", menuName = "GaramStudio/Items Inventory/Item/Consumable Item")]
public class ConsumableItemSO : ItemSO
{
    private void Awake()
    {
        ItemType = ItemType.Consumable;
    }
}
