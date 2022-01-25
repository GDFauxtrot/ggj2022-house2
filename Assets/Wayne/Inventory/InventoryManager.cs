using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public class InventoryItem{
        public ItemData item;
        public int num;

        public InventoryItem(ItemData item, int num){
            this.item = item;
            this.num = num;
        }
    }

    private List<InventoryItem> currentItems = new List<InventoryItem>();
    [Header("Inventory Page")]
    [SerializeField] private GameObject inventoryPage;
    [SerializeField] private InventoryItemList itemList;

    [Header("Preview Page")]
    [SerializeField] private Image previewEquippedActiveItemIcon;
    [SerializeField] private TextMeshProUGUI previewEquippedActiveItemNumText;
    private ItemData equippedActiveItem = null;

    void Start(){
        UpdateInventoryPreview();
    }

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
        UpdateInventoryPreview();
    }

    public void RemoveItem(ItemData item, int num){
        int index = Find(item);
        if(index >= 0)
        {
            currentItems[index].num -= num;
            if(currentItems[index].num <= 0){
                currentItems.RemoveAt(index);
                // if this item is equipped, unequip it 
                if(item == equippedActiveItem)
                    equippedActiveItem = null;
            }
        }
        UpdateInventoryPreview();
    }

    public List<InventoryItem> GetItemList(){
        return currentItems;
    }

    public int Find(ItemData item){
        for(int x = 0; x < currentItems.Count; ++x)
        {
            if(currentItems[x].item == item)
                return x;
        }
        return -1;
    }

    public int GetNum(ItemData item){
        int index = Find(item);
        if(index < 0)
            return 0;
        return currentItems[index].num;
    }

    public void OpenInventory(){
        inventoryPage.SetActive(true);
        itemList.Initialize();
    }

    public void CloseInventory(){
        inventoryPage.SetActive(false);
    }

    public void UpdateInventoryPreview(){ 
        bool equippedItem = (equippedActiveItem != null);
        previewEquippedActiveItemIcon.gameObject.SetActive(equippedItem);
        previewEquippedActiveItemNumText.gameObject.SetActive(equippedItem);
        if(equippedItem){
            previewEquippedActiveItemIcon.sprite = equippedActiveItem.sprite;
            previewEquippedActiveItemNumText.text = GetNum(equippedActiveItem).ToString();
        }
    }

    private void UpdateSingleItemUI(InventoryItemUI itemUI, InventoryItem itemInfo){
        
    }
}
