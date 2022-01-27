using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    public enum LootType{
        Money,
        Item
    }

    public enum State{
        Initialization,
        Idle,
        AttractToPlayer
    }
    private LootType type;
    private State state;
    private int moneyAmount;
    private ItemData itemData;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Collider lootCollider;
    [SerializeField] private float pickUpDistance = 10;
    [SerializeField] private float attractMovingSpeed = 40;
    private float initializationTime = 1;
    private float initializationTimer;
    private GameObject player;
    private MoneyManager moneyManager;
    private InventoryManager inventoryManager;

    // Update is called once per frame
    void Update()
    {
        initializationTimer += Time.deltaTime;
        switch(state){
            case(State.Initialization):{
                if(initializationTimer > initializationTime){
                    state = State.Idle;
                }
                break;
            }
            case(State.Idle):{
                Vector3 diff = player.transform.position - gameObject.transform.position;
                float distance = diff.magnitude;
                if(distance < pickUpDistance){
                    state = State.AttractToPlayer;
                    // change the collider to a trigger so it wont be blocked by other objects will move to player
                    lootCollider.isTrigger = true;
                    rigid.useGravity = false;
                }
                break;
            }
            case(State.AttractToPlayer):{
                Vector3 diff = player.transform.position - gameObject.transform.position;
                float distance = diff.magnitude;
                rigid.AddForce(attractMovingSpeed * Time.deltaTime * 60 * diff.normalized, ForceMode.Acceleration);
                //make sure velocity always points to the player, otherwise pickup might orbit around player
                rigid.velocity = diff.normalized * rigid.velocity.magnitude;
                break;
            }
        }
    }

    public void InitializeMoneyLoot(int moneyAmount){
        type = LootType.Money;
        this.moneyAmount = moneyAmount;
        Initialize();
    }

    public void InitializeItemLoot(ItemData itemData){
        type = LootType.Item;
        this.itemData = itemData;
        Initialize();
    }

    private void Initialize(){
        initializationTimer = 0;
        player = FindObjectOfType<PlayerController>().gameObject;
        moneyManager = FindObjectOfType<MoneyManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        state = State.Initialization;
        lootCollider.isTrigger = false;
        rigid.useGravity = true;
        // fly towards a random direction when item drops
        Vector3 force = Vector3.up + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        rigid.AddForce(force.normalized * 5, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider collider){
        if(collider.gameObject == player)
        {
            OnPickUp();
            Destroy(this.gameObject);
        }
    }

    private void OnPickUp(){
        if(type == LootType.Money)
        {
            moneyManager.AddMoney(moneyAmount);
        }else{
            inventoryManager.AddItem(itemData, 1);
        }
    }

}
