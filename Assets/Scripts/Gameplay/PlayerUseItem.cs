using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseItem : MonoBehaviour
{   
    void Update()
    {
        if(Input.GetButtonDown("UseItem")){
            UseEquippedActiveItem();
        }
    }

    private void UseEquippedActiveItem(){
        ItemData eqippedItem = InventoryManager.Instance.GetEquippedItem();
        if(eqippedItem != null && InventoryManager.Instance.GetNum(eqippedItem) > 0){
            InventoryManager.Instance.RemoveItem(eqippedItem, 1);
            ItemManager.Instance.UseItem(eqippedItem);
        }
    }
}
