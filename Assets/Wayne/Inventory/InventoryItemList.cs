using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemList : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private int maxItemPerPage;
    [SerializeField] private List<InventoryItemUI> itemUIs;
    [SerializeField] private InventoryManager manager;
    
    [Header("page")]
    [SerializeField] private GameObject pageIcon;
    private List<GameObject> pageIconList = new List<GameObject>(); 
    [SerializeField] private Transform pageIconGroup;
    [SerializeField] private Color selectedPageIconColor;
    [SerializeField] private float selectedPageIconScale;
    [SerializeField] private Color unselectedPageIconColor;

    private int page = 1;
    
    public void Initialize()
    {
        page = 1;
        UpdatePage();
        UpdatePageIcon();
    }

    // populate the items on current page
    public void UpdatePage()
    {
        List<InventoryManager.InventoryItem> items = manager.GetItemList();
        page = Mathf.Min(page, GetMaxPage());
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

    public void UpdatePageIcon()
    {
        UpdatePageIcon(GetMaxPage());
    }

    public void UpdatePageIcon(int maxPage)
    {
        // add page icons when there are not enough icons
        for(int x = pageIconList.Count; x < maxPage; ++x)
        {
            GameObject newIcon = Instantiate(pageIcon, pageIconGroup);
            pageIconList.Add(newIcon);
        }
        // remove page icons when there are too many icons
        while(pageIconList.Count > maxPage)
        {
            Destroy(pageIconList[pageIconList.Count - 1]);
            pageIconList.RemoveAt(pageIconList.Count - 1);
        }
        // set all icon
        foreach(GameObject pageIcon in pageIconList){
            pageIcon.transform.localScale = Vector3.one;
            pageIcon.GetComponent<Image>().color = unselectedPageIconColor;
        }
        pageIconList[page-1].transform.localScale = Vector3.one * selectedPageIconScale;
        pageIconList[page-1].GetComponent<Image>().color = selectedPageIconColor;
    }

    public void PageUp(){
        page = Mathf.Max(1, page - 1);
        UpdatePageIcon();
        UpdatePage();
    }

    public void PageDown(){
        page = Mathf.Min(GetMaxPage(), page + 1);
        UpdatePageIcon();
        UpdatePage();
    }

    public int GetMaxPage(){
        List<InventoryManager.InventoryItem> items = manager.GetItemList();
        return ((items.Count - 1) / maxItemPerPage) + 1;
    }
}
