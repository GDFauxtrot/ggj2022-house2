using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingAtPlayer : EnemyShootingBase
{
    private Animator animator;
    private void Start() {
        animator = GetComponentInChildren<Animator>();
    }
    protected override void Shoot()
    {
        EnemyProjectile bullet = bulletPool.GetObject().GetComponent<EnemyProjectile>();
        Vector3 direction = (player.transform.position - transform.position).normalized;
        bullet.Setup(transform.position, direction, enemyData.attack);
        animator.SetBool("HasShot", true);
    }
}
