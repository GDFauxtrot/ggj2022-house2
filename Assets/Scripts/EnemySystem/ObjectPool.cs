using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectPref;
    private HashSet<GameObject> objectsSpawned = new HashSet<GameObject>();
    private List<GameObject> pool = new List<GameObject>();
    
    public GameObject GetObject(){
        // if not enough objects in the pool, spawn new ones
        if(pool.Count <= 0){
            GameObject newObj = Instantiate(objectPref, transform);
            objectsSpawned.Add(newObj);
            pool.Add(newObj);
        }
        GameObject obj = pool[pool.Count - 1];
        pool.Remove(obj);
        obj.SetActive(true);
        return obj;
    }

    public void Recycle(GameObject obj){
        if(!objectsSpawned.Contains(obj)){
            Debug.LogError(String.Format("{0} trying to recycle {1}, which is not an object created by it.", gameObject.name, obj.name));
        }else{
            pool.Add(obj);
            obj.SetActive(false);
        }
    }


}
