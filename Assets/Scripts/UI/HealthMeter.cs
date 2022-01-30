using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeter : MonoBehaviour
{
    public GameObject healthIconPrefab;
    public PlayerController player;
    private List<GameObject> healthIcons;

    void Awake()
    {
        PlayerController.PlayerHealthChangedEvent += PlayerHurt;

        // Find player if the reference wasn't properly established
        if (!player)
        {
            Debug.LogWarning("HealthMeter: Player reference not assigned in the UI! Make sure to set it up in the scene!");
            player = GameObject.FindObjectOfType<PlayerController>();
        }

        healthIcons = new List<GameObject>();
    }

    void Start()
    {
        // Use Start for setup instead - player potentially still setting up, avoid order dependency

        // Check if everything's set up
        if (player && healthIconPrefab)
        {
            for (int i = 0; i < player.maxHealth; ++i)
            {
                // HealthMeter uses an auto-layout system, no positioning required
                healthIcons.Add(GameObject.Instantiate(healthIconPrefab, transform));
            }
        }
        else
        {
            Debug.LogError("HealthMeter: Either player or health icon prefab hasn't been setup! Not spawning health UI");
        }
    }

    void OnDestroy()
    {
        PlayerController.PlayerHealthChangedEvent -= PlayerHurt;
    }

    // Event-based actions
    void PlayerHurt(int newHealth)
    {
        int currentHealth = healthIcons.Count; // lmao

        if (newHealth < currentHealth)
        {
            GameObject.Destroy(healthIcons[healthIcons.Count-1]);
            healthIcons.RemoveAt(healthIcons.Count-1);
        }
        else
        {
            int healthAdded = newHealth - currentHealth;
            for (int i = 0; i < healthAdded; ++i)
            {
                healthIcons.Add(GameObject.Instantiate(healthIconPrefab, transform));
            }
        }
    }
}
