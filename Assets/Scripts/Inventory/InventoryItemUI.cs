using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI numText;
    [SerializeField] private GameObject selectIndicator;
    [SerializeField] private GameObject hoverIndicator;
    private InventoryManager manager;
    private ItemData itemData;

    public void Initialize(InventoryManager manager, ItemData itemData, int num){
        this.itemData = itemData;
        this.manager = manager;
        UpdateInfo(itemData, num);
    }

    public void UpdateInfo(ItemData itemData, int num){
        this.itemData = itemData;
        itemIcon.sprite = itemData.sprite;
        numText.text = num.ToString();
    }

    public void OnClick()
    {
        manager.SelectItem(this, itemData);
    }

    public void OnMouseEnter()
    {
        hoverIndicator.SetActive(true);
    }

    public void OnMouseExit()
    {
        hoverIndicator.SetActive(false);
    }

    public void SetSelectorActive(bool value){
        selectIndicator.SetActive(value);
    }
}
