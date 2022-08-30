using UnityEngine;
using UnityEngine.Events;

public class ItemDrop : MonoBehaviour
{
    public static event UnityAction<Item, int> OnTouch;
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private int amount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnTouch?.Invoke(new Item(itemSO), amount);
            Destroy(gameObject);
        }
    }
}
