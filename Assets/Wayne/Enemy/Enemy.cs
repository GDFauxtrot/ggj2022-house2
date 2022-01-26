using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    private int health;

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

    private void Die(){
        Destroy(gameObject);
    }
}
