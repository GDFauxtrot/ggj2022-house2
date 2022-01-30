using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    private int health;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;
    [Header("SoundEffects")]
    public RandomAudioPlayer HitSound;

    [Header("Moves")]
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private Transform doorTrans;
    private bool rotateToPlayer;

    void Start(){
        Initialize();
    }


    public void Initialize(){
        SceneLinkedSMB<Boss>.Initialise(animator, this);
        gameObject.SetActive(true);
        health = data.maxHealth;
        if(!player) player = FindObjectOfType<PlayerController>().gameObject;
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
    void Update(){

    }
    public void LookAtRandomDirection(){
        transform.Rotate(Vector3.up * Random.Range(0, 360f));
    }

    public void LookAtPlayer(){
        Vector3 pos = player.transform.position;
        pos.y = transform.position.y;        
        transform.LookAt(pos);
    }

    public void FinishAttacking(){
        animator.SetTrigger("FinishAttacking");
    }

    public void ShootForward(){
        Vector3 direction = transform.forward;
        // shot 3 bullets
        for(int x = -1; x <= 1; ++x){
            EnemyProjectile bullet = bulletPool.GetObject().GetComponent<EnemyProjectile>();
            bullet.Setup(doorTrans.position, Quaternion.Euler(0, 45 * x, 0) * direction, 1, bulletPool);
        }
        FinishAttacking();
    }
}
