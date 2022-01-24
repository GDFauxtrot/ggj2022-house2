using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI numText;
    private InventoryManager manager;

    public void Initialize(InventoryManager manager, ItemData itemData, int num){
        this.manager = manager;
        UpdateInfo(itemData, num);
    }

    public void UpdateInfo(ItemData itemData, int num){
        itemIcon.sprite = itemData.sprite;
        numText.text = num.ToString();
    }
}
