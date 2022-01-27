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

    void Start(){
        Initialize();
    }

    public void Initialize(){
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

    private void Die(){
        DropLoots();
        Destroy(gameObject);
    }

    private void DropLoots(){
        // drop Money
        int moneyLoot = Random.Range(data.lootMoneyMin, data.lootMoneyMax);
        if(moneyLoot > 0){
            GameObject lootObj = Instantiate(lootPref, transform.position, Quaternion.identity);
            lootObj.GetComponent<LootPickUp>().InitializeMoneyLoot(moneyLoot);
        }

        // drop Items
        foreach(LootItemData lootData in data.lootItems){
            if(Random.Range(0, 1f) <= lootData.droppingRate)
            {
                GameObject lootObj = Instantiate(lootPref, transform.position, Quaternion.identity);
                lootObj.GetComponent<LootPickUp>().InitializeItemLoot(lootData.item);
            }
        }
    }
}
