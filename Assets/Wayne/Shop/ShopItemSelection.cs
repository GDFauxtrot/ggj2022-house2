using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSelection : MonoBehaviour
{
    private ShopItem itemData;
    private ShopManager manager;
    [SerializeField] private Transform itemIconTransform;
    public void Initialize(ShopItem itemData, ShopManager manager)
    {
        this.manager = manager;
        this.itemData = itemData;
        GameObject obj = Instantiate(itemData.iconPref, itemIconTransform);
        obj.transform.localPosition = Vector3.zero;
    }

    public void Select(){
        manager.DisplayItemInfo(itemData);
    }
}
