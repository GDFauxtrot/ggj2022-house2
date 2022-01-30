using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    private int health;
    [SerializeField] private Animator animator;
    [Header("SoundEffects")]
    public RandomAudioPlayer HitSound;

    [Header("Moves")]
    [SerializeField] private float walkMinTime = 1;
    [SerializeField] private float walkMaxTime = 2;

    void Start(){
        Initialize();
    }

    public void Initialize(){
        SceneLinkedSMB<Boss>.Initialise(animator, this);
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

    private void DropLoots(){
        // drop Money
        int moneyLoot = Random.Range(data.lootMoneyMin, data.lootMoneyMax);
        for(int x = 0; x < moneyLoot; ++x){
            GameObject lootObj = EnemyManager.Instance.LootPickUpPool.GetObject();
            lootObj.GetComponent<LootPickUp>().InitializeMoneyLoot(1, transform.position + Vector3.up);
        }

        // drop Items
        foreach(LootItemData lootData in data.lootItems){
            if(Random.Range(0, 1f) <= lootData.droppingRate)
            {
                GameObject lootObj = EnemyManager.Instance.LootPickUpPool.GetObject();
                lootObj.GetComponent<LootPickUp>().InitializeItemLoot(lootData.item, transform.position + Vector3.up);
            }
        }
    }

    // Moves
    public void MoveTowardsRandomDirection(){
        float walkTime = Random.Range(walkMinTime, walkMaxTime);
        transform.Rotate(Vector3.up * Random.Range(0, 360f));
        Invoke("FinishWalking", walkTime);
    }

    public void FinishWalking(){
        animator.SetTrigger("FinishWalking");
    }
}
