using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingAtPlayer : EnemyShootingBase
{
    protected override void Shoot()
    {
        EnemyProjectile bullet = bulletPool.GetObject().GetComponent<EnemyProjectile>();
        Vector3 direction = (player.transform.position - transform.position).normalized;
        bullet.Setup(transform.position, direction, enemyData.attack);
    }
}
