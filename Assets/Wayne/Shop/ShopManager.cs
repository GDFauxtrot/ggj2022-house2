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

    void Start()
    {
        InitializeSelectionPanel();
    }

    private void InitializeSelectionPanel(){
        // only show the selection panel when there are more than 1 items
        if(itemsAvailable.Count <= 1){
            itemSelectionPanel.SetActive(false);
            return;
        }

        itemSelectionPanel.SetActive(true);
        foreach(ShopItem i in itemsAvailable){
            GameObject newObj = Instantiate(itemSelectionIconPref, itemSelectionGroup);
            ShopItemSelection itemSelection = newObj.GetComponent<ShopItemSelection>();
            itemSelection.Initialize(i);
        }
    }
}
