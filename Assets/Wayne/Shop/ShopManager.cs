using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<ShopItem> itemsAvailable = new List<ShopItem>();
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Transform itemIconTransform;
    [SerializeField] private GameObject itemSelectionPanel; 
    [SerializeField] private Transform itemSelectionGroup; 
    [SerializeField] private GameObject itemSelectionIconPref;

    private List<ShopItemSelection> itemSelectionIcons = new List<ShopItemSelection>();
    private ShopItemSelection ItemSelectionSelected;
    private ShopItem itemSelected;
    private GameObject itemIcon;

    void Start()
    {
        Open();
    }

    void Update()
    {
        if(itemIcon != null){
            itemIcon.transform.Rotate(Vector3.up * Time.deltaTime * 60);
        }
    }

    private void Open()
    {
        InitializeSelectionPanel();
        itemSelectionIcons[0].Select();
    }

    private void Close()
    {

    }

    private void InitializeSelectionPanel(){
        // only show the selection panel when there are more than 1 items
        itemSelectionPanel.SetActive(itemsAvailable.Count > 1);
        
        itemSelectionIcons.Clear();
        foreach(ShopItem i in itemsAvailable){
            GameObject newObj = Instantiate(itemSelectionIconPref, itemSelectionGroup);
            ShopItemSelection itemSelection = newObj.GetComponent<ShopItemSelection>();
            itemSelection.Initialize(i, this);
            itemSelectionIcons.Add(itemSelection);
        }
    }

    public void Select(ShopItemSelection selectionIcon, ShopItem itemData){
        if(ItemSelectionSelected != null) 
            ItemSelectionSelected.UnSelect();
        ItemSelectionSelected = selectionIcon;
        DisplayItemInfo(itemData);
    }

    //Update item info to show a new item
    public void DisplayItemInfo(ShopItem itemData){
        itemSelected = itemData;
        priceText.text = itemData.price.ToString();
        // remove previous item icon 
        if(itemIcon != null)
            Destroy(itemIcon);
        itemIcon = Instantiate(itemData.iconPref, itemIconTransform);
        itemIcon.transform.localPosition = Vector3.zero;
    }
}
