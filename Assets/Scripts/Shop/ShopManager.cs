using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Trigger")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float showTriggerDistance = 5;
    [SerializeField] private GameObject shopTiggerCanvas;
    private bool inShop = false;

    [Header("Shop UI")]
    [SerializeField] private List<ItemData> itemsAvailable = new List<ItemData>();
    [SerializeField] private GameObject ShopCanvas;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Transform itemIconTransform;
    [SerializeField] private GameObject itemSelectionPanel; 
    [SerializeField] private Transform itemSelectionGroup; 
    [SerializeField] private GameObject itemSelectionIconPref;

    private List<ShopItemSelection> itemSelectionIcons = new List<ShopItemSelection>();
    private ShopItemSelection ItemSelectionSelected;
    private ItemData itemSelected;
    private GameObject itemIcon;

    void Start(){
        InitializeSelectionPanel();
    }

    void Update()
    {
        if(inShop){
            if(itemIcon != null){
                itemIcon.transform.Rotate(Vector3.up * Time.deltaTime * 60);
            }
        }else{
            CheckShowingShopTrigger();
        }
    }

    private void CheckShowingShopTrigger(){
        if(!inShop){
            float distance = Vector3.Distance(transform.position, player.transform.position);
            shopTiggerCanvas.SetActive(distance <= showTriggerDistance);
        }
    }

    public void Open()
    {
        inShop = true;
        shopTiggerCanvas.SetActive(false);
        CameraController.SetCurrentTarget(cameraTarget);
        ShopCanvas.SetActive(true);
        itemSelectionIcons[0].Select();
    }

    public void Close()
    {
        inShop = false;
        CameraController.SetCurrentTarget(player.transform);
        ShopCanvas.SetActive(false);
    }

    private void InitializeSelectionPanel(){
        // only show the selection panel when there are more than 1 items
        itemSelectionPanel.SetActive(itemsAvailable.Count > 1);
        
        itemSelectionIcons.Clear();
        foreach(ItemData i in itemsAvailable){
            GameObject newObj = Instantiate(itemSelectionIconPref, itemSelectionGroup);
            ShopItemSelection itemSelection = newObj.GetComponent<ShopItemSelection>();
            itemSelection.Initialize(i, this);
            itemSelectionIcons.Add(itemSelection);
        }
    }

    public void Select(ShopItemSelection selectionIcon, ItemData itemData){
        if(ItemSelectionSelected != null) 
            ItemSelectionSelected.UnSelect();
        ItemSelectionSelected = selectionIcon;
        DisplayItemInfo(itemData);
    }

    //Update item info to show a new item
    public void DisplayItemInfo(ItemData itemData){
        itemSelected = itemData;
        priceText.text = itemData.price.ToString();
        // remove previous item icon 
        if(itemIcon != null)
            Destroy(itemIcon);
        itemIcon = Instantiate(itemData.iconModel, itemIconTransform);
        itemIcon.transform.localPosition = Vector3.zero;
    }
}
