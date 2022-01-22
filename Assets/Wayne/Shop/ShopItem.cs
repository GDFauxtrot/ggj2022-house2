using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Data/Shop/ShopItem", order = 1)]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public int price;
    public GameObject iconPref;
}
