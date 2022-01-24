using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSelection : MonoBehaviour
{
    [SerializeField] private Transform itemIconTransform;
    [SerializeField] private GameObject selectIndicator;
    private Item itemData;
    private ShopManager manager;
    public void Initialize(Item itemData, ShopManager manager)
    {
        this.manager = manager;
        this.itemData = itemData;
        GameObject obj = Instantiate(itemData.iconModel, itemIconTransform);
        obj.transform.localPosition = Vector3.zero;
    }

    public void Select(){
        manager.Select(this, itemData);
        selectIndicator.SetActive(true);
    }

    public void UnSelect(){
        selectIndicator.SetActive(false);
    }
}
