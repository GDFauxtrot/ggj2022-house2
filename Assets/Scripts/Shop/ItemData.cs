using UnityEngine;

public enum ItemType{
    activeItem,
    questItem
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public int price;
    public Sprite sprite;
    public GameObject iconModel;
}
