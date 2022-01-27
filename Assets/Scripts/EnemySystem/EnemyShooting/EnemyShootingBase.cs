using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyShootingBase : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPref;
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected float shootIntervalMin = 3;
    [SerializeField] protected float shootIntervalMax = 5;
    protected float shootInterval;
    protected float shootTimer = 0;
    protected ObjectPool bulletPool;
    protected GameObject player;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        shootInterval = Random.Range(shootIntervalMin, shootIntervalMax);
        bulletPool = FindObjectOfType<EnemyManager>().EnemyBulletPool;
    }

    protected virtual void Update(){
        shootTimer += Time.deltaTime;
        if(shootTimer > shootInterval){
            shootTimer = 0;
            shootInterval = Random.Range(shootIntervalMin, shootIntervalMax);
            Shoot();
        }
    }

    protected abstract void Shoot();
}
