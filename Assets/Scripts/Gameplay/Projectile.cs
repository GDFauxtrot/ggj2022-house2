using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // TODO should projectiles have prefabs/ScriptableObjects with public inspector-assigned values?
    // Probably yeah - do it later, too sleepy
    private float movementSpeed;
    private Vector3 movementDirection;

    private float damageValue;

    private float lifetime;
    private float lifetimeStart;

    private PlayerController player;

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position += movementDirection * movementSpeed * Time.deltaTime;

            if (lifetimeStart + lifetime < Time.timeSinceLevelLoad)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Setup(PlayerController source, Vector3 startPos, Vector3 direction, float speed, float maxLifetime, float damage)
    {
        player = source;
        transform.position = startPos;
        movementSpeed = speed;
        movementDirection = direction;
        lifetime = maxLifetime;
        lifetimeStart = Time.timeSinceLevelLoad;
        damageValue = damage;
    }
}
