using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: actually calculate the damage from player bullet
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    private int health;
    [Header("SoundEffects")]
    public RandomAudioPlayer HitSound;
    private Animator animator;

    void Start(){
        animator = GetComponentInChildren<Animator>();
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
        animator.SetBool("Damaged", true);
    }

    private void OnTriggerEnter(Collider collider){
        if(collider.GetComponent<Projectile>()){
            collider.gameObject.SetActive(false);
            if(HitSound) HitSound.PlayRandomClip();
            TakeDamage(5);
        }
    }

    public virtual void Die(){
        DropLoots();
        Destroy(gameObject);
    }

    private void DropLoots() {
        // All enemies drop some amount of money
        int moneyDropped = Random.Range(data.moneyDropMin, data.moneyDropMax);
        for (int x = 0; x < moneyDropped; ++x)
        {
            GameObject moneyObj = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Money);
            moneyObj.GetComponent<MoneyPickup>().Initialize(1, transform.position);
        }

        // drop loot things
        foreach (LootItemData lootData in data.lootItems)
        {
            if (Random.Range(0, 1f) <= lootData.dropChance)
            {
                GameObject lootObj = ObjectPoolManager.Instance.GetObject(ObjectPoolType.LootDrop);
                lootObj.GetComponent<LootPickup>().Initialize(lootData.item, transform.position);
            }
        }
    }
}
