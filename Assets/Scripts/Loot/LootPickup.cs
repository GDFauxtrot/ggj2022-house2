using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickup : MonoBehaviour
{
    [SerializeField] private LootMovement lootMovement;
    private ItemData itemData;

    void Awake()
    {
        if (!lootMovement)
            lootMovement = GetComponent<LootMovement>();
    }

    public void Initialize(ItemData data, Vector3 initialPosition)
    {
        itemData = data;
        lootMovement.Initialize(initialPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            InventoryManager.Instance.AddItem(itemData, 1);
            ObjectPoolManager.Instance.RecycleIntoPool(ObjectPoolType.LootDrop, gameObject);
        }
    }
}
