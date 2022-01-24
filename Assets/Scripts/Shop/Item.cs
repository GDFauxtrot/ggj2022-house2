using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public int price;
    public Sprite sprite;
    public GameObject iconModel;
}
