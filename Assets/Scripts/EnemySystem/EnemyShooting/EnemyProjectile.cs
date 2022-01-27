using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private Vector3 movementDirection;
    private float damageValue;

    [SerializeField] private float lifetime = 10f;
    private float lifetimer = 0f;

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position += movementDirection * movementSpeed * Time.deltaTime;
            lifetimer += Time.deltaTime;
            if (lifetimer > lifetime)
            {
                FindObjectOfType<EnemyManager>().EnemyBulletPool.Recycle(gameObject);
            }
        }
    }

    public void Setup(Vector3 startPos, Vector3 direction, float damage)
    {
        gameObject.SetActive(true);
        transform.position = startPos;
        movementDirection = direction;
        lifetimer = 0;
        damageValue = damage;

        transform.LookAt(transform.position + direction);
    }
}
