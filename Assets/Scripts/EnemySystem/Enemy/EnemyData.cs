using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public int maxHealth;
    public int attack;
    public float movingSpeed;
    public int lootMoneyMin;
    public int lootMoneyMax;
    public List<LootItemData> lootItems = new List<LootItemData>();
}

[System.Serializable]
public class LootItemData{
    public ItemData item;
    [Range(0, 1f)] public float droppingRate;
}