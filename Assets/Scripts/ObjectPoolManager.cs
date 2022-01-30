using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ObjectPoolType { PlayerProjectile, EnemyProjectile, Money, LootDrop };

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    // Only way I could think of without building inspector tools to assign prefabs to types, rip
    public GameObject playerProjectilePrefab;
    public GameObject enemyProjectilePrefab;
    public GameObject moneyPrefab;
    public GameObject lootDropPrefab;

    private Dictionary<ObjectPoolType, List<GameObject>> pools;
    private Dictionary<ObjectPoolType, GameObject> poolParents;
    private Dictionary<ObjectPoolType, List<GameObject>> objectsInUse;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);


        // Create pool for each possible pool type in the game
        pools = new Dictionary<ObjectPoolType, List<GameObject>>();
        poolParents = new Dictionary<ObjectPoolType, GameObject>();
        foreach (ObjectPoolType type in Enum.GetValues(typeof(ObjectPoolType)))
        {
            pools.Add(type, new List<GameObject>());
            
            GameObject parent = new GameObject(Enum.GetName(typeof(ObjectPoolType), type) + " Parent");
            parent.transform.SetParent(transform);

            poolParents.Add(type, parent);
        }

        objectsInUse = new Dictionary<ObjectPoolType, List<GameObject>>();
        foreach (ObjectPoolType type in Enum.GetValues(typeof(ObjectPoolType)))
        {
            objectsInUse.Add(type, new List<GameObject>());
        }
    }

    /// <summary>
    /// "Destroys" a given GameObject instance that belongs into a pool, putting it back in the pool for re-use.
    /// </summary>
    public void RecycleIntoPool(ObjectPoolType type, GameObject go)
    {
        if (!objectsInUse[type].Contains(go))
        {
            Debug.LogError("Wrong pool used to recycle GameObject! Pool used: " + Enum.GetName(typeof(ObjectPoolType), type));
            return;
        }

        objectsInUse[type].Remove(go);
        pools[type].Add(go);

        go.SetActive(false);
    }

    /// <summary>
    /// Returns a GameObject from the requested pool (by type). If the pool is empty, a new instance is created.
    /// </summary>
    public GameObject GetObject(ObjectPoolType type)
    {
        GameObject obj;
        if (pools[type].Count <= 0)
        {
            // Spawn new object, goes straight into use
            obj = Instantiate(GetPrefabForPoolType(type), poolParents[type].transform);
        }
        else
        {
            // Take object out of the pool
            GameObject poolObj = pools[type][pools[type].Count - 1];
            pools[type].Remove(poolObj);
            obj = poolObj;
        }

        objectsInUse[type].Add(obj);
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// Returns the corresponding inspector-assigned prefab for each ObjectPoolType type.
    /// </summary>
    private GameObject GetPrefabForPoolType(ObjectPoolType type)
    {
        switch (type)
        {
            case ObjectPoolType.PlayerProjectile:
                return playerProjectilePrefab;
            case ObjectPoolType.EnemyProjectile:
                return enemyProjectilePrefab;
            case ObjectPoolType.Money:
                return moneyPrefab;
            case ObjectPoolType.LootDrop:
                return lootDropPrefab;
        }

        // This should be impossible? idk return something valid
        return moneyPrefab;
    }
}
