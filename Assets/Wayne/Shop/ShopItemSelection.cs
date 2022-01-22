using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSelection : MonoBehaviour
{
    private ShopItem itemData;
    [SerializeField] private Transform itemIconTransform;
    public void Initialize(ShopItem itemData)
    {
        this.itemData = itemData;
        GameObject obj = Instantiate(itemData.iconPref, itemIconTransform);
        obj.transform.localPosition = Vector3.zero;
    }
}