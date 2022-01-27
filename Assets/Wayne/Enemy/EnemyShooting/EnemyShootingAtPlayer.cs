using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingAtPlayer : EnemyShootingBase
{
    protected override void Shoot()
    {
        EnemyProjectile bullet = Instantiate(bulletPref, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
        Vector3 direction = (player.transform.position - transform.position).normalized;
        bullet.Setup(transform.position, direction, enemyData.attack);
    }
}
