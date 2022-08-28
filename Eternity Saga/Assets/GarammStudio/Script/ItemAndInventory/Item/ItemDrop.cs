using UnityEngine;
using UnityEngine.Events;

public class ItemDrop : MonoBehaviour
{
    public static event UnityAction<ItemSO, int> OnTouch;
    [SerializeField] private ItemSO item;
    [SerializeField] private int amount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnTouch?.Invoke(item, amount);
            Destroy(gameObject);
        }
    }
}
