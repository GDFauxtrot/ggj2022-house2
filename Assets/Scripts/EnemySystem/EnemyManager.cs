using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private ObjectPool lootPickUpPool;
    public ObjectPool LootPickUpPool{get => lootPickUpPool; private set => lootPickUpPool = value;}
    [SerializeField] private ObjectPool enemyBulletPool;
    public ObjectPool EnemyBulletPool{get => enemyBulletPool; private set => enemyBulletPool = value;}
}
