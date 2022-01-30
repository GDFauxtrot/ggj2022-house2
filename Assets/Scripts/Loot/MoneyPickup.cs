using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    [SerializeField] private LootMovement lootMovement;
    int moneyAmount;

    void Awake()
    {
        if (!lootMovement)
            lootMovement = GetComponent<LootMovement>();
    }

    public void Initialize(int money, Vector3 initialPosition)
    {
        moneyAmount = money;
        lootMovement.Initialize(initialPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            MoneyManager.Instance.AddMoney(moneyAmount);
            ObjectPoolManager.Instance.RecycleIntoPool(ObjectPoolType.Money, gameObject);
        }
    }
}
