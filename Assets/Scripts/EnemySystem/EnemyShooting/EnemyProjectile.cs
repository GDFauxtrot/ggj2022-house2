using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private float scaledSpeed = 5f;
    private Vector3 movementDirection;
    private float damageValue;

    [SerializeField] private float lifetime = 10f;
    private float lifetimer = 0f;

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position += movementDirection * scaledSpeed * Time.deltaTime;
            lifetimer += Time.deltaTime;
            if (lifetimer > lifetime)
            {
                ObjectPoolManager.Instance.RecycleIntoPool(ObjectPoolType.EnemyProjectile, gameObject);
            }
        }
    }

    public void Setup(Vector3 startPos, Vector3 direction, float damage)
    {
        Setup(startPos, direction, damage, 1);
    }

    public void Setup(Vector3 startPos, Vector3 direction, float damage, float speedScale)
    {
        gameObject.SetActive(true);
        transform.position = startPos;
        movementDirection = direction;
        scaledSpeed = movementSpeed * speedScale;
        lifetimer = 0;
        damageValue = damage;

        transform.LookAt(transform.position + direction);
    }
}
