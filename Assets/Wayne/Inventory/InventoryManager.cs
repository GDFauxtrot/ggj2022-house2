using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private class InventoryItem{
        public ItemData item;
        public int num;

        public InventoryItem(ItemData item, int num){
            this.item = item;
            this.num = num;
        }
    }

    private List<InventoryItem> currentItems = new List<InventoryItem>();
    private ItemData equippedActiveItem = null;

    public void AddItem(ItemData item, int num){
        int index = Find(item);
        if(index >= 0)
            currentItems[index].num += num;
        else{
            currentItems.Add(new InventoryItem(item, num));
            // if this is the only active item player has, automatically equip it
            if(item.type == ItemType.activeItem && equippedActiveItem == null)
                equippedActiveItem = item;
        }
    }

    public void RemoveItem(ItemData item, int num){
        int index = Find(item);
        if(index >= 0)
        {
            currentItems[index].num -= num;
            if(currentItems[index].num <= 0)
                currentItems.RemoveAt(index);
        }
    }

    public int Find(ItemData item){
        for(int x = 0; x < currentItems.Count; ++x)
        {
            if(currentItems[x].item == item)
                return x;
        }
        return -1;
    }
}
