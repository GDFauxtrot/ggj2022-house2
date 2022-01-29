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
    private ItemData itemData;

    public void Initialize(ItemData itemData, int num){
        this.itemData = itemData;
        UpdateInfo(itemData, num);
    }

    public void UpdateInfo(ItemData itemData, int num){
        this.itemData = itemData;
        itemIcon.sprite = itemData.sprite;
        numText.text = num.ToString();
    }

    public void OnClick()
    {
        InventoryManager.Instance.SelectItem(this, itemData);
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
