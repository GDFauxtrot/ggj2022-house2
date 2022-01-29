using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> 
{
    private static T instance;
    public static T Instance {get => instance; private set => instance = value;}
    protected virtual void Awake(){
        if(instance == null){
            instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
            return;
        }
    }
}
