using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : Singleton<ItemManager>
{
    [System.Serializable]
    public class ItemEffectInfo{
        public ItemData item;
        public UnityEvent itemEffect;
    }
    
    [SerializeField] private List<ItemEffectInfo> itemEffectInfo = new List<ItemEffectInfo>();
    private Dictionary<ItemData, UnityEvent> itemEffectDict = new Dictionary<ItemData, UnityEvent>(); 

    protected override void Awake(){
        base.Awake();
        foreach(ItemEffectInfo itemInfo in itemEffectInfo){
            itemEffectDict.Add(itemInfo.item, itemInfo.itemEffect);
        }
    }

    public void UseItem(ItemData item){
        if(itemEffectDict.ContainsKey(item)){
            itemEffectDict[item].Invoke();
        }else{
            Debug.LogError(string.Format("Effect for item {0} has not been set up in ItemManager.", item.name));
        }
    }
}
