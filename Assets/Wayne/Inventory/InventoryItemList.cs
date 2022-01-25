using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemList : MonoBehaviour
{
    [SerializeField] private int maxItemPerPage;
    [SerializeField] private List<InventoryItemUI> itemUIs;
    [SerializeField] private InventoryManager manager;
    private int page = 1;
    
    void Initialize()
    {
        page = 1;
    }

    // populate the items on current page
    void UpdatePage()
    {
        List<InventoryManager.InventoryItem> items = manager.GetItemList();
        int maxPage = ((items.Count - 1) / maxItemPerPage) + 1;
        page = Mathf.Min(page, maxPage);
        int startIndex = maxItemPerPage * (page - 1);
        int displayItemNums = startIndex + maxItemPerPage >= items.Count ? items.Count - startIndex : maxItemPerPage;
        for(int i = 0; i < displayItemNums; ++i){
            InventoryManager.InventoryItem itemData = items[startIndex + i];
            itemUIs[i].gameObject.SetActive(true);
            itemUIs[i].Initialize(manager, itemData.item, itemData.num);
        }
        for(int i = displayItemNums; i < maxItemPerPage; ++i){
            itemUIs[i].gameObject.SetActive(false);
        }
    }
}
