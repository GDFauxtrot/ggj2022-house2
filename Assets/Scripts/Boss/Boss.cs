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
    [SerializeField] private Transform mapLowerLeftPoint;
    [SerializeField] private Transform mapUpperRightPoint;
    [SerializeField] private Transform doorTrans;
    [SerializeField] private Transform centerTrans;
    [SerializeField] private GameObject circleMarkPref;
    private List<GameObject> circleMarks = new List<GameObject>();
    [SerializeField] private GameObject sideSlamHurtBox;
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
            TakeDamage(1);
        }
    }

    public virtual void Die(){
        DropLoots();
        Destroy(gameObject);
    }

    private void DropLoots(){
        // drop Money
        int moneyLoot = Random.Range(data.moneyDropMin, data.moneyDropMax);
        for (int x = 0; x < moneyLoot; ++x) {
            GameObject moneyObj = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Money);
            moneyObj.GetComponent<MoneyPickup>().Initialize(1, transform.position);
        }

        // drop Items
        foreach(LootItemData lootData in data.lootItems){
            if(Random.Range(0, 1f) <= lootData.dropChance)
            {
                GameObject lootObj = ObjectPoolManager.Instance.GetObject(ObjectPoolType.LootDrop);
                lootObj.GetComponent<LootPickup>().Initialize(lootData.item, transform.position);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // get danaged when touching enemy
        if (col.collider.gameObject == player)
        {
            player.GetComponent<PlayerController>().HurtPlayerOnce();
        }
    }

    // Moves/Skills for the Boss
    public void LookAtRandomDirection(){
        Vector3 lookAtPos = new Vector3(Random.Range(mapLowerLeftPoint.transform.position.x, mapUpperRightPoint.transform.position.x), 
                                        transform.position.y,
                                        Random.Range(mapLowerLeftPoint.transform.position.z, mapUpperRightPoint.transform.position.z));
        transform.LookAt(lookAtPos);
    }

    public void LookAtPlayer(){
        Vector3 pos = player.transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);
    }

    public void ShootForward(){
        Vector3 direction = transform.forward;
        // shot 3 bullets
        for (int x = -1; x <= 1; ++x) {
            EnemyProjectile bullet = ObjectPoolManager.Instance.GetObject(ObjectPoolType.EnemyProjectile).GetComponent<EnemyProjectile>();
            bullet.transform.localScale = Vector3.one * 0.5f;
            bullet.Setup(doorTrans.position, Quaternion.Euler(0, 45 * x, 0) * direction, 1);
        }
    }

    public void ShootBulletsDown(){
        float fallAreaSize = 10;
        Vector2 bulletHeightRange = new Vector2(30f, 50f);
        for (int x = 0; x < 5; ++x) {
            // bullet fall down around player
            Vector2 xRange = new Vector2(Mathf.Max(mapLowerLeftPoint.position.x, player.transform.position.x - fallAreaSize/2), 
                                         Mathf.Min(mapUpperRightPoint.position.x, player.transform.position.x + fallAreaSize/2));
            Vector2 zRange = new Vector2(Mathf.Max(mapLowerLeftPoint.position.z, player.transform.position.z - fallAreaSize/2), 
                                         Mathf.Min(mapUpperRightPoint.position.z, player.transform.position.z + fallAreaSize/2));
            Vector3 shotPos = new Vector3(Random.Range(xRange.x, xRange.y), 
                                        0.1f,
                                        Random.Range(zRange.x, zRange.y));
            // show marks
            if(circleMarks.Count <= x)
                circleMarks.Add(Instantiate(circleMarkPref));
            GameObject mark = circleMarks[x];
            mark.SetActive(true);
            mark.transform.position = shotPos;
            // shoot bullets
            EnemyProjectile bullet = ObjectPoolManager.Instance.GetObject(ObjectPoolType.EnemyProjectile).GetComponent<EnemyProjectile>();
            bullet.transform.localScale = Vector3.one * 1f;
            bullet.Setup(shotPos + Vector3.up * Random.Range(bulletHeightRange.x, bulletHeightRange.y), Vector3.down, 1, 3.5f);
        }
    }

    public void ShootBulletsDownFinished(){
        foreach(GameObject mark in circleMarks){
            mark.SetActive(false);
        }
    }

    public void SetSideSlamHurtBoxActive(bool value){
        sideSlamHurtBox.SetActive(value);
    }

    public void ShootAround(int numOfBullets){
        Vector3 direction = transform.forward;
        float angleInterval = 360f / numOfBullets;
        // shot 3 bullets
        for (int x = 0; x < numOfBullets; ++x) {
            EnemyProjectile bullet = ObjectPoolManager.Instance.GetObject(ObjectPoolType.EnemyProjectile).GetComponent<EnemyProjectile>();
            bullet.transform.localScale = Vector3.one * 0.5f;
            bullet.Setup(centerTrans.position, Quaternion.Euler(0, angleInterval * x, 0) * direction, 1);
        }
    }
}
