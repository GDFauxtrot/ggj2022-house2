using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Data")]
    public int maxHealth;
    public float movingSpeed;
    public float targetRange;

    [Header("Money Drop")]
    public int moneyDropMin;
    public int moneyDropMax;

    [Header("Loot Drop")]
    public List<LootItemData> lootItems = new List<LootItemData>();
}

[System.Serializable]
public class LootItemData{
    public ItemData item;
    [Range(0, 1f)] public float dropChance;
}