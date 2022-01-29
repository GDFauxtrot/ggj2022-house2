using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingAtPlayer : EnemyShootingBase
{
    private Animator animator;
    protected override void Start() {
        base.Start();
        animator = GetComponentInChildren<Animator>();
    }
    protected override void Shoot()
    {
        // Simple impl - target is always "player"
        if (Vector3.Distance(transform.position, player.transform.position) < enemyData.shootingRange)
        {
            EnemyProjectile bullet = EnemyManager.Instance.EnemyBulletPool.GetObject().GetComponent<EnemyProjectile>();
            Vector3 direction = (player.transform.position - transform.position).normalized;
            bullet.Setup(transform.position, direction, enemyData.attack);
            animator.SetBool("HasShot", true);
        }
    }
}
