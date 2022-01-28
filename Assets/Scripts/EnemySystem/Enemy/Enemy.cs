using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: actually calculate the damage from player bullet
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    private int health;
    [Header("Loot")]
    [SerializeField] private GameObject lootPref;
    private ObjectPool lootPickUpPool;

    void Start(){
        Initialize();
    }

    public void Initialize(){
        lootPickUpPool = FindObjectOfType<EnemyManager>().LootPickUpPool;
        gameObject.SetActive(true);
        health = data.maxHealth;
    } 

    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            Die();
        }
    }

    private void OnTriggerEnter(Collider collider){
        if(collider.GetComponent<Projectile>()){
            collider.gameObject.SetActive(false);
            TakeDamage(5);
        }
    }

    public virtual void Die(){
        DropLoots();
        Destroy(gameObject);
        // transform.position = Vector3.zero;
        // Initialize();
    }

    private void DropLoots(){
        // drop Money
        int moneyLoot = Random.Range(data.lootMoneyMin, data.lootMoneyMax);
        for(int x = 0; x < moneyLoot; ++x){
            GameObject lootObj = lootPickUpPool.GetObject();
            lootObj.GetComponent<LootPickUp>().InitializeMoneyLoot(1, transform.position + Vector3.up);
        }

        // drop Items
        foreach(LootItemData lootData in data.lootItems){
            if(Random.Range(0, 1f) <= lootData.droppingRate)
            {
                GameObject lootObj = lootPickUpPool.GetObject();
                lootObj.GetComponent<LootPickUp>().InitializeItemLoot(lootData.item, transform.position + Vector3.up);
            }
        }
    }
}
