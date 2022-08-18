using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static Action<ItemSO, int> OnTouch;
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
